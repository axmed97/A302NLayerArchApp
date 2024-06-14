using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.SubCategoryDTOs;


namespace Business.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL)
        {
            _subCategoryDAL = subCategoryDAL;
        }

        public IResult Create(AddSubCategoryDTO model)
        {
            try
            {
                _subCategoryDAL.Add(new()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                });
                return new SuccessResult("Ugurlan elave olundu!", System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
