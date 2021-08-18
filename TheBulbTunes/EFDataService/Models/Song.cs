using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBulbTunes.EFDataService.Models
{
   public class Song
    {
      
        [Required]
        public Guid SongId { get; set; }
       
        [Required, MaxLength(100)]
        public string Title { get; set; }
        
        [Required, MaxLength(100)]
        public string Artist { get; set; }
        
        [Required, MaxLength(100)]
        public string Album { get; set; }
       
        [MaxLength(250)]
        public string Featuring { get; set; }
        
        [MaxLength(250)]
        public string Genre { get; set; }
       
        [Required]
        public DateTime ReleaseDate { get; set; }

        // Favourites belonging to this song
        public List<Favorite> FavoritesList { get; set; }
    }
}
