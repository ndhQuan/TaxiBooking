using AutoMapper;
using TaxiBooking.Models;
using TaxiBooking.Models.DTO;

namespace TaxiBooking
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
            CreateMap<Taxi, TaxiCreateDTO>().ReverseMap();
            CreateMap<Taxi, TaxiDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateUpdateDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateUpdatePartialDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateCreateDTO>().ReverseMap();
            CreateMap<JourneyLog, JourneyDTO>().ReverseMap();
            //CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
        }
    }
}
