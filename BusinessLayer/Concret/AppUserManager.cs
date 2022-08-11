using BusinessLayer.Abstract;
using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Concret
{
    public class AppUserManager : BaseManager, IAppUserService
    {
        private readonly SignInManager<User> signInManager;

        public AppUserManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            signInManager = serviceProvider.GetService<SignInManager<User>>();
        }

        public async Task<IResult<IList<User>>> GetAllAsync()
        {
            var entities = await userManager.Users
                .Include(a => a.Addresses).ToListAsync();

            if (entities is null) return Error<IList<User>>("No Data Available On Request");
            return Success(entities);
        }

        public async Task<IResult<User>> GetByIdAsync(int id)
        {
            var entity = await userManager.Users.Include(a => a.Addresses).SingleOrDefaultAsync(p => p.Id == id);
            if (entity is null) return Error<User>("Not Found Code General Message");
            return Success(entity);
        }
    }
}
