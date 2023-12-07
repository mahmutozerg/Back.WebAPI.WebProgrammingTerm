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
        var entity = await _productRepository
            .Where(p => p != null && p.Id == productUpdateDto.TargetProductId && !p.IsDeleted)
            .Include(x=>x.Company)
            .FirstOrDefaultAsync();

        if (entity is null)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        entity.Price = productUpdateDto.Price == 0f ? entity.Price : productUpdateDto.Price;
        entity.Name =  string.IsNullOrWhiteSpace(productUpdateDto.Name) ? entity.Name : productUpdateDto.Name;
        entity.ImagePath =  string.IsNullOrWhiteSpace(productUpdateDto.ImagePath) ? entity.ImagePath : productUpdateDto.ImagePath;
        entity.Stock = productUpdateDto.Stock == 0 ? entity.Stock : productUpdateDto.Stock;
        entity.DiscountRate = productUpdateDto.DiscountRate == 0f ? entity.DiscountRate : productUpdateDto.DiscountRate;
        entity.UpdatedBy = updatedBy;
        _productRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Product>.Success(entity, ResponseCodes.Updated);
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