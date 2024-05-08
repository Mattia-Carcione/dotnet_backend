using Entities.Model;

namespace Dtos.Author
{
    public class UpdateAuthorRequestDto
    {
        
        public string Author_Name { get; set; }
        public string Author_Surname { get; set; }
        public DateOnly Author_DateOfBirthhday { get; set; }
        
    }
}
