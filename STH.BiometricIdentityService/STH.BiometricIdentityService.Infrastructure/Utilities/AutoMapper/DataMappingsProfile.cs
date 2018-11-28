using AutoMapper;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.AutoMapper
{
    public class DataMappingsProfile : Profile
    {
        private readonly IMapper _mapper;

        DataMappingsProfile(IMapper mapper)
        {
            _mapper = mapper;
        }
        //protected override void Configure()
        //{
        //    _mapper.Map<Domain.Objects.Client, Client>()
        //        .ForMember(x => x.ClientAppId, opt => opt.MapFrom(x => x.AppId))
        //        .ForMember(x => x.APIKey, opt => opt.MapFrom(x => x.ApiKey))
        //        .ForMember(x => x.DeviceUniqueId, opt => opt.MapFrom(x => x.DeviceUId))
        //        .ForMember(x => x.LastLoggedInDate, opt => opt.MapFrom(x => x.LastAccessedDate));

        //    Mapper.CreateMap<Client, Domain.Objects.Client>()
        //        .ForMember(x => x.AppId, opt => opt.MapFrom(x => x.ClientAppId))
        //        .ForMember(x => x.ApiKey, opt => opt.MapFrom(x => x.APIKey))
        //        .ForMember(x => x.DeviceUId, opt => opt.MapFrom(x => x.DeviceUniqueId))
        //        .ForMember(x => x.LastAccessedDate, opt => opt.MapFrom(x => x.LastLoggedInDate));


        //}
    }
}