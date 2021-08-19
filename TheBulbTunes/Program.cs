﻿using System;
using System.Linq;
using TheBulbTunes.EFDataService;
using TheBulbTunes.EFDataService.Models;
using System.Collections;
using System.Collections.Generic;
using TheBulbTunes.EFDataService.Services;

namespace TheBulbTunes
{
    class Program
    {
        static void Main(string[] args)
        {
           SongServices songService  = new SongServices();

            // Create a number songs
            songService.Create("All Over", "Tiwa Savage", "Sugarcane", "", "Afro-pop,Romantic", new DateTime(2017, 5, 22));
            songService.Create("Nobody's Business", "Rihanna", "Unapologetic", "Chris Brown", "R & B", new DateTime(2012, 1, 1));
            songService.Create("Get Down on it", "Kool & The Gang", "Something Special", "", "Funk", new DateTime(1981, 11, 22));
            songService.Create("The Monster", "Eminem", "Marshall Matters", "Rihanna", "R&B/Rap", new DateTime(2013, 1, 1));
            songService.Create("Essence", "Wizkid", "Made In Lagos", "Tems", "R&B", new DateTime(2020, 10, 30));


            //Fetch all song before updates
            List<Song> availableSongs = songService.FetchAll();
            Console.WriteLine("Title\tArtist\tAlbum");
            foreach (Song song in availableSongs)
            {
                Console.WriteLine();
                Console.WriteLine($"{song.Title}\t{song.Artist}\t{song.Album}");
            }


            // Fetch songs that meet some search criteria
            List<Song> filteredSongs1 = songService.FetchWithFilter("Over", "Romantic", null, null);
            List<Song> filteredSongs2 = songService.FetchWithFilter("Ess", "R", "Lagos", "Kid");
            List<Song> filteredSongs3 = songService.FetchWithFilter();
            List<Song> filteredSongs4 = songService.FetchWithFilter();


            Console.WriteLine("\n\nFILTERED SONGS FOR JANE\n");
            Console.Write("Title\t\tArtist\t\tAlbum");
            foreach (Song song in filteredSongs1)
            {
                Console.WriteLine();
                Console.Write($"{song.Title}\t{song.Artist}\t{song.Album}");
            }

            Console.WriteLine("\n\nFILTERED SONGS FOR HOPE\n");
            Console.Write("Title\t\tArtist\t\tAlbum");
            foreach (Song song in filteredSongs2)
            {
                Console.WriteLine();
                Console.Write($"{song.Title}\t{song.Artist}\t{song.Album}");

            }

            // Set the id of song to be updated
            Guid idOfSongToUpdate1 = new Guid("0fc59e9e-c4eb-43b6-3dc3-08d9625747ad"); // Non-existing id
            Guid idOfSongToUpdate2 = new Guid("0fc59e9e-c4eb-43b6-3dc3-08d9625747ae"); // Existing id

            // Create a Song object containing new info for the update
            Song songWithNewInfo = new Song()
            {
                Genre = "Rap/Hip-hop",
                ReleaseDate = new DateTime(2013, 1, 1)
            };

            // Call the Update method to make the desired update
            songService.Update(idOfSongToUpdate1, songWithNewInfo);
            songService.Update(idOfSongToUpdate2, songWithNewInfo);

            // Fetch all songs after update
            availableSongs = songService.FetchAll();

            Console.WriteLine("\n\nCURRENTLY AVAILABLE SONGS:\n");
            Console.Write("Title\t\tArtist\t\tAlbum");
            foreach (Song song in availableSongs)
            {
                Console.WriteLine();
                Console.Write($"{song.Title}\t{song.Artist}\t{song.Album}");
            }


            // Set the id of song to be deleted
            Guid idOfSongToDelete1 = new Guid("05e4b13c-1fe2-4ef6-aa68-08d96238e7ab"); // Non-existing id
            Guid idOfSongToDelete2 = new Guid("597b4637-92c8-4c34-aa67-08d96238e7ac"); // Existing id

            // Call the Delete method to perform the desired deletion
            songService.Delete(idOfSongToDelete1);
            songService.Delete(idOfSongToDelete2);

            // Fetch all songs after delete
            availableSongs = songService.FetchAll();

            Console.WriteLine("\n\nCURRENTLY AVAILABLE SONGS:\n");
            Console.Write("Title\t\tArtist\t\tAlbum");
            foreach (Song song in availableSongs)
            {
                Console.WriteLine();
                Console.Write($"{song.Title}\t{song.Artist}\t{song.Album}");
            }


        }

    }
    }

