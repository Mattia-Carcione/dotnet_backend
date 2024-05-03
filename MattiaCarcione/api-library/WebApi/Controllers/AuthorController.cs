using Microsoft.AspNetCore.Mvc;
using LibraryModel.Model;
using LibraryInterface.Interfaces;
using AutoMapper;
using LibraryDtos.Dtos.Author;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _authorRepository.GetAllAsync();

            var authorsMapped = response.Select(author => _mapper.Map<AuthorDto>(author)).ToList();

            return Ok(authorsMapped);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            var response = await _authorRepository.GetByIdAsync(id);

            var authorMapped = _mapper.Map<AuthorDetailDto>(response);

            return Ok(authorMapped);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AuthorDto author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authorMapped = _mapper.Map<Author>(author);

            var response = await _authorRepository.CreateAsync(authorMapped);

            var responseMapped = _mapper.Map<AuthorDto>(response);

            return Ok(responseMapped);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AuthorDto author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authorMapped = _mapper.Map<Author>(author);

            var response = await _authorRepository.UpdateAsync(id, authorMapped);

            var responseMapped = _mapper.Map<AuthorDto>(response);

            return Ok(responseMapped);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _authorRepository.DeleteAsync(id);

            return Ok(response);
        }
    }
}
