using BusinessLayer.Abstract;
using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Concret
{
    public class AppRoleManager : BaseManager, IAppRoleService
    {
        public AppRoleManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _roleManager = serviceProvider.GetService<RoleManager<Role>>();
        }

        private readonly RoleManager<Role> _roleManager;

        public async Task<IResult<IList<Role>>> GetAllAsync()
        {
            var entities = await _roleManager.Roles.ToListAsync();
            if (entities == null) return Error<IList<Role>>("No Data Available On Request");

            return Success(entities);
        }
    }
}
