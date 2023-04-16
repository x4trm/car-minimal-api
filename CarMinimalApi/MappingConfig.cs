using AutoMapper;
using CarMinimalApi.Models;
using CarMinimalApi.Models.DTO;

namespace CarMinimalApi
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        { 
            CreateMap<Car,CarAddDTO>().ReverseMap();
            CreateMap<Car,CarUpdateDTO>().ReverseMap();
        }
    }
}
