using Entities.Model;
namespace Dtos.BookDtos
{
    public class UpdateBookRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public int NumberOfPages { get; set; }
        public DateOnly DateOfPublication { get; set; }
        public int NumberOfTotalCopies { get; set; }
        public int NumberOfCopiesLeft { get; set; }

        //FK
        public int? Publisher_Id { get; set; }
        public int? Author_Id { get; set; }

    }
}
