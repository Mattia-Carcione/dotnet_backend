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
        CreateMap<Book, BookDetailDTO>()
            .ForPath(
                dest => dest.Author,
                opt => opt.MapFrom(src => $"{src.Author.Name} {src.Author.LastName}")
            )
            .ForPath(dest => dest.Editor, opt => opt.MapFrom(src => src.Editor.Name))
            .ReverseMap();

        CreateMap<CreateBookDTO, Book>().ReverseMap();
        CreateMap<BookDTO, Book>().ReverseMap();
        CreateMap<UpdateBookDTO, Book>().ReverseMap();
        CreateMap<BookingDTO, Booking>().ReverseMap();
        CreateMap<BookingDetailDTO, Booking>().ReverseMap();
        CreateMap<CategoryDTO, Category>().ReverseMap();
    }
}
