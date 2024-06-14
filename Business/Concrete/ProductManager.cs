using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.ProductDTOs;
using System.Net;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<IResult> CreateAsync(AddProductDTO model)
        {
            await _productDAL.CreateProductAsync(model);
            return new SuccessResult(HttpStatusCode.Created);
        }

        public IDataResult<GetProductDTO> GetById(Guid id, string langCode)
        {
            var data = _productDAL.GetProduct(id, langCode);
            if (data == null)
                return new ErrorDataResult<GetProductDTO>(message: "Məlumatlar səhvdir yenidən cəhd edin", HttpStatusCode.NotFound);
            if (data.IsStock == false)
                return new ErrorDataResult<GetProductDTO>(message: "", HttpStatusCode.BadRequest);

            return new SuccessDataResult<GetProductDTO>(data, HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateAsync(Guid id, UpdateProductDTO model)
        {
            await _productDAL.UpdateProductAsync(id, model);
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
