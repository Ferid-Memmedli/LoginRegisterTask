using EntitiesLayer.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EntitiesLayer.Concrete.Identity
{
    public class Role : IdentityRole<int>, IEntity
    {
    }
}