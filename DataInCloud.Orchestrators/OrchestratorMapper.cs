using AutoMapper;
using DataInCloud.Orchestrators.Car.Contract;

namespace DataInCloud.Orchestrators;

public class OrchestratorMapper : Profile
{
    public OrchestratorMapper()
    {
        CreateMap<Model.Car.Car, Car.Contract.Car>()
            .ReverseMap();

        CreateMap<CreateCar, Model.Car.Car>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ReverseMap();

        CreateMap<EditCar, Model.Car.Car>().ReverseMap();
    }
}
