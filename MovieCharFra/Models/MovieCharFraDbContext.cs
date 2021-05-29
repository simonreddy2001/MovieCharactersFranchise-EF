using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharFra.Models
{
    public class MovieCharFraDbContext : DbContext
    {
        public MovieCharFraDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; } 
        public DbSet<Franchise> Franchises { get; set; }



        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= PC1681\\SQLEXPRESS; Initial Catalog = MovCharFraDB; Integrated Security = true;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "The Godfather", Genre = "Crime", Year = 1972, Director = "Francis", Picture = "https://www.imdb.com/title/tt0068646/mediaviewer/rm746868224/", Trailer = "https://www.imdb.com/video/vi1348706585?playlistId=tt0068646&ref_=tt_pr_ov_vi", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 2, Title = "The Godfather Part2", Genre = "Crime", Year = 1974, Director = "Francis", Picture = "https://www.imdb.com/title/tt0071562/mediaviewer/rm4159262464/", Trailer = "https://www.imdb.com/video/vi696162841?playlistId=tt0071562&ref_=tt_pr_ov_vi", FranchiseId = 1 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 3, Title = "The Lord of the Rings: The Return of the King", Genre = "Adventure", Year = 2003, Director = "Peter", Picture = "https://www.imdb.com/title/tt0167260/mediaviewer/rm584928512/", Trailer = "https://www.imdb.com/video/vi2073101337?playlistId=tt0167260&ref_=tt_pr_ov_vi", FranchiseId = 2 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 4, Title = "The Lord of the Rings: The Fellowship of the Ring", Genre = "Drama", Year = 2001, Director = "Peter Jackson", Picture = "https://www.imdb.com/title/tt0120737/mediaviewer/rm3592958976/", Trailer = "https://www.imdb.com/video/vi2073101337?playlistId=tt0120737&ref_=tt_pr_ov_vi", FranchiseId = 2 });

            modelBuilder.Entity<Character>().HasData(new Character { Id = 1, Name = "Ian McKellen", Alias = "Gandalf", Gender = "Male" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 2, Name = "Elijah Wood", Alias = "Frodo", Gender = "Male" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 3, Name = "Marlon Brando", Alias = "Don", Gender = "Male" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 4, Name = "Al Pacino", Alias = "Michael", Gender = "Male" });

            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Name = "The Lord of the Rings", Description = "Michael" });
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Name = "The Godfather", Description = "Michael"  });

            modelBuilder.Entity<Character>()
                .HasMany(m => m.Movies)
                .WithMany(c => c.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    mi => mi.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    ci => ci.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    mc =>
                    {
                        mc.HasKey("CharacterId", "MovieId");
                        mc.HasData(
                            new { MovieId = 1, CharacterId = 1 },
                            new { MovieId = 1, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 1 },
                            new { MovieId = 2, CharacterId = 2 },
                            new { MovieId = 3, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 4 },
                            new { MovieId = 4, CharacterId = 3 },
                            new { MovieId = 4, CharacterId = 4 }
                        );
                    });
   
        }
    }
}
