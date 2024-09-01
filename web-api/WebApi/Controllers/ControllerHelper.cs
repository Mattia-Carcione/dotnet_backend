using AutoMapper;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Metadatas;
using System.Linq.Expressions;
using System.Text.Json;

namespace WebApi.Controllers
{
    /// <summary>
    /// An instance of <see cref="ControllerHelper{T, TDTO, TDetailDTO}"/> provides helper methods for searching item from the current context.
    /// <para>
    /// This class extends <see cref="ControllerBase"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="T">Represents the type of the <typeparamref name="T"/> entity.</typeparam>
    /// <typeparam name="TDTO">Represents the DTO of the <typeparamref name="TDTO"/> entity.</typeparam>
    /// <typeparam name="TDetailDTO">Represents the DTO of the <typeparamref name="T"/> entity showing the entity details.</typeparam>
    public class ControllerHelper<T, TDTO, TDetailDTO> : ControllerBase 
        where T : class 
        where TDTO : class
        where TDetailDTO : class
    {
        /// <summary>
        /// A mapper object that maps entities to each other. 
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// The maximum number of the item allowed per page.
        /// </summary>
        protected const int MaxPageSize = 25;

        /// <summary>
        /// The repository interface that provides the CRUD operation methods.
        /// </summary>
        protected readonly IExtendedRepository<T> _repository;

        /// <summary>
        /// Intializes a new instance of the <see cref="ControllerHelper{T, TDTO, TDetailDTO}"/> using the mapper object and the repository interface.
        /// </summary>
        /// <param name="mapper">A mapper object that maps entities to each other.</param>
        /// <param name="repository">The repository interface that provides the CRUD operation methods.</param>
        public ControllerHelper(IMapper mapper, IExtendedRepository<T> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Gets the item from the current context using the specified id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="query">
        /// A <see cref="Func{T, TResult}"/> that takes LINQ operations.
        /// <para>
        /// Sorting must be provided.
        /// </para>
        /// </param>
        /// <returns>
        /// A task representing asynchronous operation that returns <typeparamref name="TDTO"/>.
        /// </returns>
        protected async Task<TDetailDTO?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>> query)
        {
            var entity = await _repository.GetAsync(id, query);

            if (entity == null)
                return null;

            var mappedEntity = _mapper.Map<TDetailDTO>(entity);

            return mappedEntity;
        }

        /// <summary>
        /// Gets collection of <typeparamref name="TDTO"/> and appends pagination-related data to the response header.
        /// </summary>
        /// <param name="query">
        /// The <see cref="Func{T, TResult}"/> that takes LINQ operations.
        /// <para>
        /// Sorting must be provided.
        /// </para>
        /// </param>
        /// <param name="expression">A <see cref="Expression{TDelegate}"/> that takes a <see cref="Func{T, TResult}"/> including <see cref="IQueryable{T}"/> LINQ operations.</param>
        /// <param name="pageNumber">The number of the current page.</param>
        /// <param name="pageSize">The number of the item per page.</param>
        /// <param name="firtsString">A <see cref="String"/> query param.</param>
        /// <param name="secondString">A <see cref="String"/> query param.</param>
        /// <returns>
        /// A task representing asynchronous operation that returns <see cref="IEnumerable{T}"/>.
        /// </returns>
        protected async Task<IEnumerable<TDTO>> GetDataAsync(Func<IQueryable<T>, IQueryable<T>> query,
            Expression<Func<T, bool>> expression,
            int pageNumber,
            int pageSize,
            string? firtsString = null,
            string? secondString = null)
        {
            pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            (IEnumerable<T>, PaginationMetadata) data;

            if (string.IsNullOrEmpty(firtsString) && string.IsNullOrEmpty(secondString))
            {
                data = await _repository.GetAllAsync(pageNumber, pageSize, query);
            }
            else
            {
                firtsString = firtsString?.Trim();
                secondString = secondString?.Trim();

                data = await _repository.SearchByCriteriaAsync(pageNumber, pageSize, expression, query);
            }

            var (collection, paginationMetadata) = data;

            var mappedCollection = _mapper.Map<IEnumerable<TDTO>>(collection);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return mappedCollection;
        }
    }
}
