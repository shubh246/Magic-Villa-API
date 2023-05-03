using AutoMapper;
using MagivVilla_Web.Models;

namespace MagivVilla_Web
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDto>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberCreateDto>().ReverseMap();




        }
    }
}
