using AutoMapper;
using BookCatalog.Domain.Entity;
using BookCatalog.Domain.Entity.Dto;

namespace BookCatalog
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x => {
                x.CreateMap<Book, BookDto>().ReverseMap();
                x.CreateMap<Genre, GenreDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
