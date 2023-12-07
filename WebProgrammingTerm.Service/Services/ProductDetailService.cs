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
    private readonly IDepotRepository _depotRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ProductDetailService(IUnitOfWork unitOfWork,IProductDetailRepository productDetailRepository, IProductRepository productRepository, IDepotRepository depotRepository) : base(productDetailRepository, unitOfWork)
    {
        _productDetailRepository = productDetailRepository;
        _productRepository = productRepository;
        _depotRepository = depotRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<ProductDetail>> AddAsync(ProductDetailAddDto productDetailAddDto, string createdBy)
    {
        var productEntity = await _productRepository
            .Where(p => p.Id == productDetailAddDto.ProductId && !p.IsDeleted)
            .FirstOrDefaultAsync();

        if (productEntity is null)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        var depotEntity = await _depotRepository
            .Where(p => p.Id == productDetailAddDto.DepotId && !p.IsDeleted)
            .FirstOrDefaultAsync();

        if (depotEntity is null)
            throw new Exception(ResponseMessages.DepotNotFound);

         
        var productDetailEntity = ProductDetailMapper.toProductDetail(productDetailAddDto);
        
        productDetailEntity.Depot = depotEntity;
        productDetailEntity.ProductId = productEntity.Id;
        productDetailEntity.CreatedBy = createdBy;
        productDetailEntity.UpdatedBy = createdBy;
        await _productDetailRepository.AddAsync(productDetailEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<ProductDetail>.Success(productDetailEntity, ResponseCodes.Created);
    }

    public async Task<CustomResponseDto<ProductDetail>> UpdateAsync(ProductDetailUpdateDto productDetailUpdateDto, string updatedBy)
    {
        var entity = await _productDetailRepository
            .Where(pd => pd.Id == productDetailUpdateDto.ProductDetailId && !pd.IsDeleted)
            .FirstOrDefaultAsync();

        if (entity is null)
            throw new Exception(ResponseMessages.ProductDetailNotFound);


        entity.Author = string.IsNullOrWhiteSpace(productDetailUpdateDto.Author) ? entity.Author : productDetailUpdateDto.Author;
        entity.PublishDate = string.IsNullOrWhiteSpace(productDetailUpdateDto.PublishDate.ToString()) ? entity.PublishDate : productDetailUpdateDto.PublishDate;
        entity.Publisher = string.IsNullOrWhiteSpace(productDetailUpdateDto.Publisher) ? entity.Publisher : productDetailUpdateDto.Publisher;
        entity.Language = string.IsNullOrWhiteSpace(productDetailUpdateDto.Language) ? entity.Language : productDetailUpdateDto.Language;
        entity.Size = string.IsNullOrWhiteSpace(productDetailUpdateDto.Size) ? entity.Size : productDetailUpdateDto.Size;
        entity.Category = string.IsNullOrWhiteSpace(productDetailUpdateDto.Category) ? entity.Category : productDetailUpdateDto.Category;
        entity.Page = productDetailUpdateDto.Page == 0 ? entity.Page : productDetailUpdateDto.Page;

        _productDetailRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<ProductDetail>.Success(entity, ResponseCodes.Updated);
    }
}