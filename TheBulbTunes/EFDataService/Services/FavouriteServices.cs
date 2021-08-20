using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBulbTunes.EFDataService.Models;

namespace TheBulbTunes.EFDataService.Services
{
   public class FavouriteServices
    {
        
        private readonly TheBulbTunesContext _context = new TheBulbTunesContext();
        private List<Favourite> _favourites;

        //Create a favourite
        public void Create(Guid songId, Guid userId)
        {
            // Retrieve the objects associated with the songId and userId supplied as parameters
            Song song = _context.Songs.Find(songId);
            User user = _context.Users.Find(userId);

            Favourite newFavourite = new Favourite()
            {
                Id = new Guid(),
                DateAdded = DateTime.Now,
                SelectedSong = song,
                AddedBy = user
                // The properties below need not be assigned directly because their associated navigation objects have been assigned above.
                // SelectedSongId = songId,
                // AddedById = userId,
            };

            _context.Favourites.Add(newFavourite);
            _context.SaveChanges();
        }


        //Fetch all favourites.
        public List<Favourite> FetchAll()
        {
            return _context.Favourites
                .Include(f => f.AddedBy)
                .Include(f => f.SelectedSong)
                .ToList();
        }

        //Fetch favourites filtered by: song, user and/or artist.
        public List<Favourite> FetchWithFilter(string titleFilter = null, string userFilter = null, string artistFilter = null)
        {
            // Retrieve all available favourites unfiltered
            _favourites = FetchAll();

            // If any filter is specified, apply that filter by calling its search method

            if (titleFilter != null)
                _favourites = SearchByTitle(titleFilter, _favourites);

            if (userFilter != null)
                _favourites = SearchByUser(userFilter, _favourites);

            if (artistFilter != null)
                _favourites = SearchByArtist(artistFilter, _favourites);

            return _favourites;
        }


        // Private helper methods to simplify searching with various parameters

        private List<Favourite> SearchByTitle(string searchValue, List<Favourite> favourites)
        {
            return favourites.Where(f => f.SelectedSong.Title.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Favourite> SearchByArtist(string searchValue, List<Favourite> favourites)
        {
            return favourites.Where(f => f.SelectedSong.Artist.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Favourite> SearchByUser(string searchValue, List<Favourite> favourites)
        {
            return favourites
                .Where(f =>
                    f.AddedBy.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    f.AddedBy.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        // Delete a favourite.
        public void Delete(Guid id)
        {
            // Check if a favourite with the supplied id exists
            Favourite favouriteToDelete = FetchAll()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            // The above lines can be shortened by just calling the Find() method as below.
            // favouriteToDelete = _context.Favourites.Find(id);

            if (favouriteToDelete == null)
            {
                Console.WriteLine($"Invalid operation! No match was found for the id you supplied.");
                return;
            }

            // A matching song was found. Perform the requested deletion.
            _context.Favourites.Remove(favouriteToDelete);
            _context.SaveChanges();
        }
    }
}
