using EntitiesLayer.Concrete.Identity;
using SharedLayer.Utilities.Abstract;
using System.Security.Claims;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        Task<IResult<bool>> LoginAsync(User model);
        Task<IResult<bool>> RegisterAsync(User model, string[] addresses);
        Task<IResult<IList<string>>> GetRolesAsync(ClaimsPrincipal userClaim);
        Task<IResult<User>> GetUsersAsync(ClaimsPrincipal userClaim);
        Task LogoutAsync();
    }
}