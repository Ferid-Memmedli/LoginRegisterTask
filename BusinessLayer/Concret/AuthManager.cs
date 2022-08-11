using BusinessLayer.Abstract;
using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedLayer.Constants;
using SharedLayer.Utilities.Abstract;
using System.Security.Claims;

namespace BusinessLayer.Concret
{
    public class AuthManager : BaseManager, IAuthService
    {
        public AuthManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _signInManager = serviceProvider.GetService<SignInManager<User>>();
        }

        private readonly SignInManager<User> _signInManager;

        public async Task<IResult<IList<string>>> GetRolesAsync(ClaimsPrincipal userClaim)
        {
            var user = await userManager.GetUserAsync(userClaim);
            var roles = await userManager.GetRolesAsync(user);
            return Success(roles);
        }

        public async Task<IResult<User>> GetUsersAsync(ClaimsPrincipal userClaim)
        {
            return Success(await userManager.GetUserAsync(userClaim));
        }

        public async Task<IResult<bool>> LoginAsync(User model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Error<bool>("Already Exist User");
            await _signInManager.SignOutAsync();

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!signInResult.Succeeded) return Error<bool>("Login Failed");
            return Success(true);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IResult<bool>> RegisterAsync(User model, string[] addresses)
        {
            bool isAlreadyRegister = await userManager.Users.AnyAsync(i => i.NormalizedEmail == model.Email.ToUpper()
                                                                      || i.NormalizedUserName == model.UserName.ToUpper());
            if (isAlreadyRegister) return Error<bool>("Already Exist User");
            if (addresses != null)
            {
                foreach (var name in addresses)
                {
                    model.Addresses.Add(new EntitiesLayer.Concrete.Entities.Address() { Name = name, UserId = model.Id });
                }
            }

            var identityResult = await userManager.CreateAsync(model, model.Password);
            if (!identityResult.Succeeded)
            {
                var identityErrors = identityResult.Errors.Select(i => i.Description).ToArray();
                return Error<bool>(identityErrors);
            }
            var roleResult = await userManager.AddToRoleAsync(model, RoleConstant.Guest);
            if (!roleResult.Succeeded)
            {
                var Errors = roleResult.Errors.Select(i => i.Description).ToArray();
                var deleteResult = await userManager.DeleteAsync(model);
                if (!deleteResult.Succeeded)
                {
                    var errors = deleteResult.Errors.Select(i => i.Description).ToArray();
                    return Error<bool>(errors);
                }
                return Error<bool>(Errors);
            }
            return Success(true, "Created Successfully");
        }
    }
}
