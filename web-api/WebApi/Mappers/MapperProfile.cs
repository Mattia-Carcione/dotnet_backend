//TODO:
//usare AutoMapper per passare dai DTO alle entità e viceversa

using AutoMapper;
using DTOs.BookDTOs;
using DTOs.BookingDTOs;
using DTOs.CategoryDTOs;
using Model.Entities;

namespace WebApi.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Book, BookDTO>()
            .ReverseMap();

        CreateMap<CreateBookDTO, Book>()
            .ReverseMap();
    }
}
