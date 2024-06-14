using Core.Utilities.Results.Abstract;
using Entities.DTOs.SubCategoryDTOs;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        IResult Create(AddSubCategoryDTO model);
    }
}
