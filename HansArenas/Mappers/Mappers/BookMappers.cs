using Entities.Model;
using Dtos.BookDtos;


namespace Mapper.Books
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                BookId = bookModel.BookId,
                Title = bookModel.Title,
                NumberOfPages = bookModel.NumberOfPages,
                DateOfPublication = bookModel.DateOfPublication,
                NumberOfTotalCopies = bookModel.NumberOfTotalCopies,
                NumberOfCopiesLeft = bookModel.NumberOfCopiesLeft,
                Publisher_Id = bookModel.Publisher_Id,
                Author_Id = bookModel.Author_Id,
                Categories_InTheBook = bookModel.Categories_InTheBook,
                Bookings = bookModel.Bookings,

                
                
                
            };
        }


        public static Book ToBookFromCreateBookDto( this CreateBookRequestDto BookDto)
        {
            return new Book

            {
                Title = BookDto.Title,
                NumberOfPages = BookDto.NumberOfPages,
                DateOfPublication = BookDto.DateOfPublication,
                NumberOfTotalCopies = BookDto.NumberOfTotalCopies,
                NumberOfCopiesLeft = BookDto.NumberOfCopiesLeft,
                Publisher_Id = BookDto.Publisher_Id,
                

            };
        }
    }
}
