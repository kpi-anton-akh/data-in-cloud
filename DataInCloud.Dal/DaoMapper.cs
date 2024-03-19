using AutoMapper;
using DataInCloud.Dal.Car;

namespace DataInCloud.Dal;

public class DaoMapper : Profile
{
    public DaoMapper()
    {
        CreateMap<CarDao, Model.Car.Car>()
            .ReverseMap();
    }
}
