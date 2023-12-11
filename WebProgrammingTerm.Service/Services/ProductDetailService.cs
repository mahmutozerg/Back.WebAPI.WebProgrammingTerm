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

public class ProductDetailService:GenericService<ProductDetail>,IProductDetailService
{
    private readonly IProductDetailRepository _productDetailRepository;
    private readonly IDepotService _depotService;
    private readonly IUnitOfWork _unitOfWork;


    public ProductDetailService(IUnitOfWork unitOfWork, IProductDetailRepository productDetailRepository, IDepotService depotService) : base(productDetailRepository, unitOfWork)
    {
        _productDetailRepository = productDetailRepository;
        _depotService = depotService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetail> CreateAsync(ProductDetailAddDto productDetailAddDto, ClaimsIdentity  claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //var productEntity = await _productService.GetProductWithCompany(productDetailAddDto.ProductId);

        var depotEntity = await _depotService
            .Where(p => p != null && p.Id == productDetailAddDto.DepotId && !p.IsDeleted)
            .FirstOrDefaultAsync();

        if (depotEntity is null)
            throw new Exception(ResponseMessages.DepotNotFound);

         
        var productDetailEntity = ProductDetailMapper.toProductDetail(productDetailAddDto);
        
        productDetailEntity.Depot = depotEntity;
        //productDetailEntity.ProductId = productEntity.Id;
        productDetailEntity.CreatedBy = createdBy;
        productDetailEntity.UpdatedBy = createdBy;

        return productDetailEntity;
    }

    public async Task<CustomResponseDto<ProductDetail>> UpdateAsync(ProductDetailUpdateDto productDetailUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var productDetailEntity = await _productDetailRepository
            .Where(pd => pd != null && pd.Id == productDetailUpdateDto.ProductDetailId && !pd.IsDeleted)
            .FirstOrDefaultAsync();

        if (productDetailEntity is null)
            throw new Exception(ResponseMessages.ProductDetailNotFound);


        productDetailEntity.Author = string.IsNullOrWhiteSpace(productDetailUpdateDto.Author) ? productDetailEntity.Author : productDetailUpdateDto.Author;
        productDetailEntity.PublishDate = string.IsNullOrWhiteSpace(productDetailUpdateDto.PublishDate) ? productDetailEntity.PublishDate : productDetailUpdateDto.PublishDate;
        productDetailEntity.Publisher = string.IsNullOrWhiteSpace(productDetailUpdateDto.Publisher) ? productDetailEntity.Publisher : productDetailUpdateDto.Publisher;
        productDetailEntity.Language = string.IsNullOrWhiteSpace(productDetailUpdateDto.Language) ? productDetailEntity.Language : productDetailUpdateDto.Language;
        productDetailEntity.Size = string.IsNullOrWhiteSpace(productDetailUpdateDto.Size) ? productDetailEntity.Size : productDetailUpdateDto.Size;
        productDetailEntity.Page = string.IsNullOrWhiteSpace(productDetailUpdateDto.Page) ? productDetailEntity.Page : productDetailUpdateDto.Page;
        productDetailEntity.UpdatedAt = DateTime.Now;
        productDetailEntity.UpdatedBy = updatedBy;
        _productDetailRepository.Update(productDetailEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<ProductDetail>.Success(productDetailEntity, ResponseCodes.Updated);
    }
}