using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete.Entities;
using Microsoft.Extensions.DependencyInjection;
using SharedLayer.Utilities.Abstract;


namespace BusinessLayer.Concret
{
    public class AddressManager : BaseManager, IAddressService
    {
        protected readonly IAddressDal addressManager;

        public AddressManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            addressManager = serviceProvider.GetService<IAddressDal>();

        }

        public async Task<IResult<IList<Address>>> GetAllAsync()
        {
            var entities = await addressManager.GetAllAsync(null, u => u.User);
            if (entities == null) return Error<IList<Address>>("No Data Available On Request");
            return Success(entities);
        }
    }
}
