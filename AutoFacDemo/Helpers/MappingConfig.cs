using AutoFacDemo.Dtos;
using AutoFacDemo.Models;
using AutoMapper;

namespace AutoFacDemo.Helpers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BookDto, Book>().ForMember(x => x.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
            });

            return mappingConfig;
        }
    }
}
