using EntitiesLayer.Concrete.Identity;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Abstract
{
    public interface IAppRoleService
    {
        Task<IResult<IList<Role>>> GetAllAsync();
    }
}
