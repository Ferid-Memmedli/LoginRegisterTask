using EntitiesLayer.Abstract;
using EntitiesLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer.Concrete.Identity
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            Addresses = new HashSet<Address>();
        }


        public string LastName { get; set; }

        public string FirstName { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public bool RememberMe { get; set; }

        // relations
        public virtual ICollection<Address> Addresses { get; set; }

    }
}