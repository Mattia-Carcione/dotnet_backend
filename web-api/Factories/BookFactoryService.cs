using Interfaces;
using Microsoft.AspNetCore.Http;
using Models.Entities;
using System.Security.Claims;

namespace Factories
{
    /// <summary>
    /// A new instance of <see cref="BookFactoryService"/> creating a service of type <see cref="IBookService"/>.
    /// </summary>
    public class BookFactoryService : IFactoryService<IBookService>
    {
        /// <summary>
        /// An object of <see cref="IServiceProvider"/>.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// An object of <see cref="IHttpContextAccessor"/>.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// An object of <see cref="IExtendedRepository{T}"/>.
        /// </summary>
        private readonly IExtendedRepository<User> _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="BookFactoryService"/>.
        /// </summary>
        /// <param name="repository">An object of <see cref="IExtendedRepository{T}"/>.</param>
        /// <param name="serviceProvider">An object of <see cref="IServiceProvider"/>.</param>
        /// <param name="httpContextAccessor">An object of <see cref="IHttpContextAccessor"/>.</param>
        public BookFactoryService(IExtendedRepository<User> repository, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }

        /// <summary>
        /// Creates a new instance of <see cref="IBookService"/>.
        /// </summary>
        /// <returns>
        /// A task operation representing asynchronous operation that returns:
        /// <list type="bullet">
        /// <item>
        /// <see cref="IPremiumServiceBook"/> if the authenticated user is a premium member,
        /// </item>
        /// <item>
        /// <see cref="IBookService"/> if the authenticated user is a standard member.
        /// </item>
        /// </list>
        /// </returns>
        public async Task<IBookService> CreateService()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            if(email != null)
            {
                var user = await _repository.SearchEntityByCriteriaAsync(u => u.Where(e => e.Email == email));

                if (user != null && user.IsPremium)
                    return _serviceProvider.GetService(typeof(IPremiumServiceBook)) as IPremiumServiceBook ?? throw new InvalidOperationException();
            }

            return _serviceProvider.GetService(typeof(IBookService)) as IBookService ?? throw new InvalidOperationException();
        }
    }
}
