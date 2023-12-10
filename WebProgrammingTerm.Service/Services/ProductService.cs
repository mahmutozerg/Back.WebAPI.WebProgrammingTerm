using System.Security.Claims;
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
    private readonly ICompanyUserService _companyUserService;
    private readonly ICompanyService _companyService;
    private readonly IProductDetailService _productDetailService;
    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository , ICompanyUserService companyUserService, ICompanyService companyService, IProductDetailService productDetailService) : base(productRepository, unitOfWork)
    {
        _productRepository = productRepository;
        _companyUserService = companyUserService;
        _companyService = companyService;
        _productDetailService = productDetailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productEntity = await GetProductWithCompany(productUpdateDto.TargetProductId);

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

    public async Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto,ClaimsIdentity claimsIdentity)
    {
        var productEntity = ProductMapper.ToProduct(productAddDto);
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (claimsIdentity.FindAll(ClaimTypes.Role).Any(role => role.Value == "Company"))
        {
            var companyName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            if (companyName is not null)
            {
                var company = await _companyService.Where(c => c != null && c.Name == companyName && !c.IsDeleted)
                    .SingleOrDefaultAsync();
                productEntity.Company = company;

            }
            else
                throw new Exception(ResponseMessages.CompanyNotFound);
            
        }
        else
        {
            var companyUserEntity = await _companyUserService.GetCompanyUserWithCompany(createdBy);
            productEntity.Company = companyUserEntity.Company;
        }
        
        var productDetailEntity = await _productDetailService.CreateAsync(ProductDetailMapper.toProductDetail(productAddDto),claimsIdentity);
        productEntity.ImagePath = productAddDto.ImagePath;
        productEntity.CreatedBy = createdBy;
        productEntity.UpdatedAt = DateTime.Now;
        productEntity.UpdatedBy = createdBy;
        productEntity.ProductDetail = productDetailEntity;
        productEntity.Category = productAddDto.Category;
        await _productRepository.AddAsync(productEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Product>.Success(productEntity, ResponseCodes.Created);

    }

    public async Task<Product> GetProductWithCompany(string productId)
    {
        var product = await _productRepository
            .Where(p => p != null && p.Id == productId && !p.IsDeleted)
            .Include(p=>p.Company)
            .SingleOrDefaultAsync();

        if (product is null)
            throw new Exception(ResponseMessages.ProductNotFound);

        return product;


    }
}