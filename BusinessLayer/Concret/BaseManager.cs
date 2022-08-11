using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Concret
{
    public abstract class BaseManager : BaseServiceResult
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected readonly UserManager<User> userManager;

        public User CurrentUser => userManager.GetUserAsync(httpContextAccessor.HttpContext?.User).Result;

        public BaseManager(IServiceProvider serviceProvider)
        {
            httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            userManager = serviceProvider.GetService<UserManager<User>>();
        }
    }
}
