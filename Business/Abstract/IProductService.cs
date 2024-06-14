using Core.Utilities.Results.Abstract;
using Entities.DTOs.ProductDTOs;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> CreateAsync(AddProductDTO model);
        Task<IResult> UpdateAsync(Guid id, UpdateProductDTO model);
        IDataResult<GetProductDTO> GetById(Guid id, string langCode);
    }
}
