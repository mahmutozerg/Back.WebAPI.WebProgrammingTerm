using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class ProductService:GenericService<Product>,IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyService _companyService;
    private readonly IProductDetailService _productDetailService;
    private readonly IUserCommentRepository _userCommentRepository;
    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository , ICompanyService companyService, IProductDetailService productDetailService, IUserCommentRepository userCommentRepository) : base(productRepository, unitOfWork)
    {
        _productRepository = productRepository;
        _companyService = companyService;
        _productDetailService = productDetailService;
        _userCommentRepository = userCommentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productEntity = await GetProductWithCompany(productUpdateDto.TargetProductId);

        if (productEntity is null)
            throw new Exception(ResponseMessages.ProductNotFound);

        ProductMapper.Update(productUpdateDto, ref productEntity);
        productEntity.UpdatedBy = updatedBy;
        
        _productRepository.Update(productEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Product>.Success(productEntity, ResponseCodes.Updated);
    }

    public async Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto,ClaimsIdentity claimsIdentity)
    {
        var productEntity = ProductMapper.ToProduct(productAddDto);
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (!claimsIdentity.FindAll(ClaimTypes.Role).Any(role => role.Value == "Company"))
            return CustomResponseDto<Product>.Fail(ResponseMessages.Notfound, ResponseCodes.NotFound);
        
        
        var companyName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

        if (companyName is not null)
        {
            var company = await _companyService.Where(c => c != null && c.Name == companyName && !c.IsDeleted)
                .SingleOrDefaultAsync();
            productEntity.Company = company;
            var productDetailEntity = await _productDetailService.CreateAsync(ProductDetailMapper.ToProductDetail(productAddDto),claimsIdentity);
        
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
        throw new Exception(ResponseMessages.CompanyNotFound);
            


    }

    public async Task<Product?> GetProductWithCompany(string productId)
    {
        var product = await _productRepository
            .Where(p => p != null && p.Id == productId)
            .Include(p=>p.ProductDetail)
            .Include(p=>p!.Company)
            .SingleOrDefaultAsync();
        return product;
    }
    public async Task<CustomResponseListDataDto<ProductGetDto>> GetProductsByPage(int page)
    {
        var products = await _productRepository.GetProducstByPage(page);
        var  dtos = products.Select(product => ProductMapper.ToAddDto(product)).ToList();
        
        return  CustomResponseListDataDto<ProductGetDto>.Success(dtos,200);
    }

    public async Task<CustomResponseListDataDto<ProductGetDto>> GetProductByName(int page=1 ,string name="")
    {
        var products = await _productRepository.GetProductsByName(page, name);
        var  dtos = products.Select(product => ProductMapper.ToAddDto(product)).ToList();
        return  CustomResponseListDataDto<ProductGetDto>.Success(dtos,200);
    }

    public async Task<CustomResponseDto<ProductWCommentDto>> GetProductWithComments(string id)
    {
        var products = await _productRepository.Where(p => p != null  && p.Id == id)
            .Include(pd=>pd.ProductDetail)
            .Include(p => p.Company).SingleOrDefaultAsync();

        if (products is null)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        var comments = await _userCommentRepository.Where(u => u != null && u.ProductId == id && !u.IsDeleted).ToListAsync();
        if (comments is null)
            throw new Exception(ResponseMessages.ProductNotFound);

        return CustomResponseDto<ProductWCommentDto>.Success(ProductMapper.Enhance(products, comments),
            ResponseCodes.Ok);
    }

    public async Task<CustomResponseNoDataDto> DeleteProductById(string id)
    {
        var product = await _productRepository.Where(p => p != null && p.Id == id && !p.IsDeleted)
            .Include(p=>p.ProductDetail)
            .SingleOrDefaultAsync();

        if (product is null)
            return CustomResponseNoDataDto.Fail(ResponseCodes.NotFound,ResponseMessages.ProductNotFound);


        product.IsDeleted = true;
        product.ProductDetail.IsDeleted = true;

        _productRepository.Update(product);
        await _unitOfWork.CommitAsync();
        
        return CustomResponseNoDataDto.Success(ResponseCodes.Ok);
    }
}