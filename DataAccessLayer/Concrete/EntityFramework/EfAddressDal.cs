using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using EntitiesLayer.Concrete.Entities;
using SharedLayer.DataAccess.Concrete.EntityFramework;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfAddressDal : EfEntityRepository<Address>, IAddressDal
    {
        private readonly LoginDbContext context;

        public EfAddressDal(LoginDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
