using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBulbTunes.EFDataService.Models;

namespace TheBulbTunes.EFDataService.Services
{
    public class SongServices
    {
        private readonly TheBulbTunesContext _context = new TheBulbTunesContext();
        private List<Song> _songs = new List<Song> { };


        //Create a song

        public void Create(string title, string artist, string album, string featuring, string genre, DateTime releaseDate)
        {
            Song newSong = new Song()
            {
                SongId = new Guid(),
                Title = title,
                Artist = artist,
                Album = album,
                Featuring = featuring,
                Genre = genre,
                ReleaseDate = releaseDate
            };

            _context.Songs.Add(newSong);
            _context.SaveChanges();
        }

        // Fetch all songs
        public List<Song> FetchAll()
        {
            return _context.Songs.ToList();
        }


        //Fetch songs with filter(Title, Genre, Album, Artist, ReleaseDate)
        public List<Song> FetchWithFilter(string titleFilter = null, string genreFilter = null, string albumFilter = null, string artistFilter = null)
        {
            // Retrieve all available songs unfiltered
            _songs = FetchAll();

            // If any filter is specified, apply that filter by calling its search method

            if (titleFilter != null)
                _songs = SearchByTitle(titleFilter, _songs);

            if (genreFilter != null)
                _songs = SearchByGenre(genreFilter, _songs);

            if (albumFilter != null)
                _songs = SearchByAlbum(albumFilter, _songs);

            if (artistFilter != null)
                _songs = SearchByArtist(artistFilter, _songs);

            return _songs;
        }

        private List<Song> SearchByTitle(string searchValue, List<Song> songs)
        {
            return songs.Where(s => s.Title.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Song> SearchByArtist(string searchValue, List<Song> songs)
        {
            return songs.Where(s => s.Artist.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Song> SearchByAlbum(string searchValue, List<Song> songs)
        {
            return songs.Where(s => s.Album.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private List<Song> SearchByGenre(string searchValue, List<Song> songs)
        {
            return songs.Where(s => s.Genre.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        //Update song
        //Delete song
    }
}
