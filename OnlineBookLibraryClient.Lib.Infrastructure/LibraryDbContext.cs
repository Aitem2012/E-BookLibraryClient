using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure
{
    public class LibraryDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedAdmin(builder);

            builder.Entity<Author>().HasData(
                                             new Author
                                             {
                                                 Id = 1,
                                                 FirstName = "William",
                                                 LastName = "Shakespeare"
                                             }
                                         );
            builder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    GenreName = "Fictional"
                },
                new Genre
                {
                    Id =2,
                    GenreName ="Non-Fictional"
                }
                );
            builder.Entity<Publisher>().HasData(
                new Publisher
                {
                    Id = 1,
                    PublisherName ="University Press"
                },
                new Publisher
                {
                    Id =2,
                    PublisherName = "MacMillian Press Ltd"
                }
                );
            builder.Entity<Book>().HasData(
                new Book { Id = 1, AuthorId = 1, Title = "Hamlet", GenreId =1, PublisherId = 1, ISBN="2787498230", Language="English" },
                new Book { Id = 2, AuthorId = 1, Title = "King Lear", GenreId = 1, PublisherId = 2, ISBN = "2787437787", Language="Spanish" },
                new Book { Id = 3, AuthorId = 1, Title = "Othello", GenreId = 2, PublisherId = 1, ISBN = "898327893484", Language="Chinese" }
            );

        }

        private void SeedAdmin(ModelBuilder builder)
        {
            //Create Roles
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed Users
            var passwordHasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                    FirstName = "Ayobami",
                    LastName = "Fadeni",
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<AppUser>().HasData(user);

            // Seed UserRoles
            IdentityUserRole<string> userRole = new IdentityUserRole<string> 
            {
                UserId = user.Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "admin123");
            
            builder.Entity<IdentityUserRole<string>>().HasData(userRole);
        }

    }
}
