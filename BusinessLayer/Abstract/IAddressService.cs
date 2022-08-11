using EntitiesLayer.Concrete.Entities;
using SharedLayer.Utilities.Abstract;

namespace BusinessLayer.Abstract
{
    public interface IAddressService
    {
        Task<IResult<IList<Address>>> GetAllAsync();
    }
}