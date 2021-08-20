using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBulbTunes.EFDataService.Models
{
    public class User
    {
        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(50)]
        public string LastName { get; set; }
       
        [Required, MaxLength(150)]
        public string EmailAddress { get; set; }

        // Favourites belonging to this user
        public List<Favourite> FavoritesList { get; set; }
        
    }
}

