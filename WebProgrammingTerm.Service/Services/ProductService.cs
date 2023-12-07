using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class ProductService:GenericService<Product>,IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyUserRepository _companyUserRepository;
    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICompanyUserRepository companyUserRepository) : base(productRepository, unitOfWork)
    {
        _productRepository = productRepository;
        _companyUserRepository = companyUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto, string updatedBy)
    {
        var productEntity = await _productRepository
            .Where(p => p != null && p.Id == productUpdateDto.TargetProductId && !p.IsDeleted)
            .Include(x=>x.Company)
            .FirstOrDefaultAsync();

        if (productEntity is null)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        productEntity.Price = productUpdateDto.Price == 0f ? productEntity.Price : productUpdateDto.Price;
        productEntity.Name =  string.IsNullOrWhiteSpace(productUpdateDto.Name) ? productEntity.Name : productUpdateDto.Name;
        productEntity.ImagePath =  string.IsNullOrWhiteSpace(productUpdateDto.ImagePath) ? productEntity.ImagePath : productUpdateDto.ImagePath;
        productEntity.Stock = productUpdateDto.Stock == 0 ? productEntity.Stock : productUpdateDto.Stock;
        productEntity.DiscountRate = productUpdateDto.DiscountRate == 0f ? productEntity.DiscountRate : productUpdateDto.DiscountRate;
        productEntity.UpdatedBy = updatedBy;
        
        _productRepository.Update(productEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Product>.Success(productEntity, ResponseCodes.Updated);
    }

    public async Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto, string createdBy)
    {
        var companyUserEntity =
            await _companyUserRepository
                .Where(cu => cu != null  && cu.UserId == createdBy && !cu.IsDeleted)
                .Include(cu=>cu.Company)
                .FirstOrDefaultAsync();

        if (companyUserEntity is null)
            throw new Exception(ResponseMessages.CompanyUserNotFound);

        var productEntity = ProductMapper.ToProduct(productAddDto);
        productEntity.Company = companyUserEntity.Company;
        productEntity.ImagePath = productAddDto.ImagePath;
        productEntity.CreatedBy = createdBy;
        productEntity.UpdatedAt = DateTime.Now;
        productEntity.UpdatedBy = createdBy;
        await _productRepository.AddAsync(productEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Product>.Success(productEntity, ResponseCodes.Created);

    }

  
}