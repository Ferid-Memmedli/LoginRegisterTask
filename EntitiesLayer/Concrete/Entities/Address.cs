using EntitiesLayer.Abstract;
using EntitiesLayer.Concrete.Identity;

namespace EntitiesLayer.Concrete.Entities
{
    public class Address : EntityBase<int>, IEntity
    {
        public string Name { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}