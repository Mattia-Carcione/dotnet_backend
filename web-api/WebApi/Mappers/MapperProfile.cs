//TODO:
//usare AutoMapper per passare dai DTO alle entità e viceversa

using AutoMapper;
using DTOs.BookDTOs;
using DTOs.BookingDTOs;
using DTOs.CategoryDTOs;
using Models.Entities;

namespace WebApi.Mappers;

/// <summary>
/// An instance of <see cref="MapperProfile"/> extends <see cref="Profile"/>.
/// <para>
/// This class creates the mapping operation between the entities.
/// </para>
/// </summary>
public class MapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of <see cref="MapperProfile"/> creating the mapping operation between the entities.
    /// </summary>
    public MapperProfile()
    {
        CreateMap<Book, BookDetailDTO>()
            .ForPath(
                dest => dest.Author,
                opt => opt.MapFrom(src => $"{src.Author!.Name} {src.Author.LastName}")
            )
            .ForPath(dest => dest.Editor, opt => opt.MapFrom(src => src.Editor!.Name))
            .ReverseMap();

        CreateMap<CreateBookDTO, Book>().ReverseMap();
        CreateMap<BookDTO, Book>().ReverseMap();
        CreateMap<UpdateBookDTO, Book>().ReverseMap();

        CreateMap<BookingDTO, Booking>().ReverseMap();
        CreateMap<BookingDetailDTO, Booking>().ReverseMap();

        CreateMap<CategoryDTO, Category>().ReverseMap();
    }
}
