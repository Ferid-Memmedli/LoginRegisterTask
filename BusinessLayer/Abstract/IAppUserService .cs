using EntitiesLayer.Concrete.Identity;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Abstract
{
    public interface IAppUserService
    {
        Task<IResult<IList<User>>> GetAllAsync();
        Task<IResult<User>> GetByIdAsync(int id);
    }
}