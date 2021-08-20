using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBulbTunes.EFDataService.Models;

namespace TheBulbTunes.EFDataService
{
   public  class TheBulbTunesContext:DbContext
    {
       public string connectionString;
        //Constructor to set up the connection to the DB
        public TheBulbTunesContext()
        {
           connectionString = @"Data Source =DESKTOP-P4C2M1N\SQLEXPRESS;Initial Catalog=TheBulbTunesDB;Integrated Security=True;Pooling=False";
        }


        // DBset properties, one for each entity/model

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Favourite>Favourites { get; set; }

        // OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseSqlServer(connectionString);

        }

        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            // Set Id as the primary key for Favourite entity
            builder.HasKey(f => f.Id);

            // A Favourite must have one Song as SelectedSong
            // Conversely, a Song can appear multiple times as a Favourite
            builder.HasOne(f => f.SelectedSong)
                .WithMany(s => s.FavoritesList)
                .HasForeignKey(f => f.SelectedSongId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            // A Favourite must have one User as AddedBy
            // Conversely, a User can have multiple Favourites
            builder.HasOne(f => f.AddedBy)
                .WithMany(u => u.FavoritesList)
                .HasForeignKey(f => f.AddedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

            public void Configure(EntityTypeBuilder<User> builder) 
        { 
                //Set UserId as the primary key for User entity
            builder.HasKey(u => u.UserId);

        }

        
        public void Configure(EntityTypeBuilder<Song> builder)
        { // Set SongId as the primary key for Song entity
            builder.HasKey(s => s.SongId);

        }
    }
}
