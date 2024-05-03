using AutoMapper;
using LibraryModel.Model;
using LibraryDtos.Dtos.Author;
using LibraryDtos.Dtos.Book;
using LibraryDtos.Dtos.Category;
using LibraryDtos.Dtos.Booking;
using LibraryDtos.Dtos.Publisher;

namespace WebApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDetailDto>();
            CreateMap<AuthorDetailDto, Author>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDetailDto, Book>();
            CreateMap<Book, BookDetailDto>();
            CreateMap<BookToCreateDto, Book>();
            CreateMap<Book, BookToCreateDto>();

            CreateMap<Booking, BookingDto>();
            CreateMap<Booking, BookingDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryDetailDto, Category>();

            CreateMap<Publisher, PublisherDto>();
            CreateMap<Publisher, PublisherDetailDto>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<PublisherDetailDto, Publisher>();
        }
    }
}
