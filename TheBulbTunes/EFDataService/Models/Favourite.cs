using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBulbTunes.EFDataService.Models
{
   public class Favourite
    {
        [Required]
        public Guid Id { get; set; }
  
        [Required]
        public Guid  SelectedSongId { get; set; }
        
        [Required]
        public Guid AddedById { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // The following are navigation properties made possible by the foreign-key relationship
        public virtual Song SelectedSong { get; set; }
        public virtual User AddedBy { get; set; }
        
    }
}
