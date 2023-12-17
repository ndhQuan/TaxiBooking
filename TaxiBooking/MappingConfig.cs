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
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Taxi, TaxiCreateDTO>().ReverseMap();
            CreateMap<Taxi, TaxiDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateUpdateDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateUpdatePartialDTO>().ReverseMap();
            CreateMap<DriverState, DriverStateCreateDTO>().ReverseMap();
            CreateMap<JourneyLog, JourneyDTO>().ReverseMap();
            CreateMap<JourneyLog, JourneyCreateDTO>().ReverseMap();
            CreateMap<JourneyLog, JourneyUpdatePartialDTO>().ReverseMap();

            //CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
        }
    }
}
