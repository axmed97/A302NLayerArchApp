using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BrandDTOs;
using Serilog;
using System.Net;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDAL _brandDAL;
        private readonly IMapper _mapper;

        public BrandManager(IBrandDAL brandDAL, IMapper mapper)
        {
            _brandDAL = brandDAL;
            _mapper = mapper;
        }

        public IResult Create(AddBrandDTO model)
        {
            var map = _mapper.Map<Brand>(model);
            _brandDAL.Add(map);
            return new SuccessResult(HttpStatusCode.Created);
        }

        public IDataResult<GetBrandDTO> Get(Guid id)
        {
            var data = _brandDAL.Get(x => x.Id == id);
            if (data == null)
            {
                Log.Error("Tapilmadi");
                return new ErrorDataResult<GetBrandDTO>(HttpStatusCode.NotFound);
            }

            var map = _mapper.Map<GetBrandDTO>(data);
            return new SuccessDataResult<GetBrandDTO>(map, HttpStatusCode.OK);
        }

        public IResult SoftDelete(Guid id)
        {

            var data = _brandDAL.Get(x => x.Id == id);
            if (data == null)
                return new ErrorResult(HttpStatusCode.NotFound);

            data.IsDeleted = true;
            _brandDAL.Update(data);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult Update(Guid id, UpdateBrandDTO model)
        {
            var data = _brandDAL.Get(x => x.Id == id);
            if (data == null)
                return new ErrorResult(HttpStatusCode.NotFound);

            var map = _mapper.Map<Brand>(model);
            _brandDAL.Update(map);
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
