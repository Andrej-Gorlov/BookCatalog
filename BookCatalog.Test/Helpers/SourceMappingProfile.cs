using AutoMapper;
using BookCatalog.Domain.Entity;
using BookCatalog.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Test.Helpers
{
    internal class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
