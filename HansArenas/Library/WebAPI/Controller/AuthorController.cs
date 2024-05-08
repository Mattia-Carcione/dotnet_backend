using Dtos.Author;
using Mapper.Authors;
using Entities.Data;
using Intefaces.crud.entities;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaPicard_API.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly Library_DbContext _context;
        private readonly IAuthorRepository _authorRepo;
        public AuthorController(Library_DbContext context, IAuthorRepository authorRepository)
        {
            _authorRepo = authorRepository;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepo.GetAllAsync();
            var authorDto = authors.Select(s => s.ToAuthorDto()); //passo da Author a AuthorDto
            return Ok(authorDto);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author.ToAuthorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequestDto authorDto)
        {
            var authorModel = authorDto.ToAuthorFromCreateAuthorDto();
            await _authorRepo.CreateAsync(authorModel);
            return CreatedAtAction(nameof(GetById), new { id = authorModel.AuthorId }, authorModel.ToAuthorDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateDto)
        {
            var authorModel = await _authorRepo.UpdateAsync(id, updateDto);

            if (authorModel == null)
            {
                return NotFound();
            }


            return Ok(authorModel.ToAuthorDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var authorModel = await _authorRepo.DeleteAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
