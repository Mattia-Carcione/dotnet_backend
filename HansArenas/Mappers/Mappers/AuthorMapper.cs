using Dtos.Author;
using Entities.Model;

namespace Mapper.Authors
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(this Author authorModel)
        {
            return new AuthorDto() 
            { 
                AuthorId = authorModel.AuthorId,
                Author_Name = authorModel.Author_Name,
                Author_Surname = authorModel.Author_Surname,
                Author_DateOfBirthhday = authorModel.Author_DateOfBirthhday,
            }
            ;
        }


        public static Author ToAuthorFromCreateAuthorDto(this CreateAuthorRequestDto AuthorDto)
        {
            return new Author()
            {
                Author_Name = AuthorDto.Author_Name,
                Author_Surname = AuthorDto.Author_Surname,
                Author_DateOfBirthhday = AuthorDto.Author_DateOfBirthhday,


            };
        }
    }
}
