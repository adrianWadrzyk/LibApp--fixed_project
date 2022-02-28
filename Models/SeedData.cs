using System;
using System.Linq;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);
                else
                    Console.WriteLine("Database already seeded with Customers.");

                if (!context.Genre.Any())
                    SeedGenres(context);
                else
                    Console.WriteLine("Database already seeded with Genres.");

                if (!context.Books.Any())
                    SeedBooks(context);
                else
                    Console.WriteLine("Database already seeded with Books.");

                if (!context.Roles.Any())
                    SeedRoles(context);
                else
                    Console.WriteLine("Database already seeded with roles.");

                if (!context.Customers.Any())
                    SeedCustomers(context, userManager);
                else
                    Console.WriteLine("Database already seeded with Books.");
            }
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
             new MembershipType
             {
                 Id = 1,
                 Name = "Pay as You Go",
                 SignUpFee = 0,
                 DurationInMonths = 0,
                 DiscountRate = 0
             },
             new MembershipType
             {
                 Id = 2,
                 Name = "Monthly",
                 SignUpFee = 30,
                 DurationInMonths = 1,
                 DiscountRate = 10
             },
             new MembershipType
             {
                 Id = 3,
                 Name = "Quaterly",
                 SignUpFee = 90,
                 DurationInMonths = 3,
                 DiscountRate = 15
             },
             new MembershipType
             {
                 Id = 4,
                 Name = "Yearly",
                 SignUpFee = 300,
                 DurationInMonths = 12,
                 DiscountRate = 20
             });

            context.SaveChanges();

        }
        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Name = "Fault in our stars",
                    AuthorName = "John Green",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 2,
                    Name = "The fellowship of the Ring",
                    AuthorName = "J.R.R. Tolkien",
                    ReleaseDate = DateTime.Parse("29/07/1954"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 99
                },
                new Book
                {
                    GenreId = 3,
                    Name = "Dune",
                    AuthorName = "Frank Herbert",
                    ReleaseDate = DateTime.Parse("01/08/1965"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 3
                });

            context.SaveChanges();

        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Sci-Fi"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Criminal"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Biography"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Horror"
                }
            );

            context.SaveChanges();

        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "user"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "StoreManager",
                        NormalizedName = "storemanager"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Owner",
                        NormalizedName = "owner"
                    }
                );

            context.SaveChanges();

        }

        private static void SeedCustomers(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();
            var c1 = new Customer
            {
                Name = "Adrian Wądrzyk",
                Email = "adrian@wadrzyk.pl",
                NormalizedEmail = "adrian@wadrzyk.pl",
                UserName = "adrian@wadrzyk.pl",
                NormalizedUserName = "adrian@wadrzyk.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c1).Wait();
            userManager.AddToRoleAsync(c1, "owner").Wait();

            var c2 = new Customer
            {
                Name = "Jan Kowalski",
                Email = "jan@kowalski.pl",
                NormalizedEmail = "jan@kowalski.pl",
                UserName = "jan@kowalski.pl",
                NormalizedUserName = "jan@kowalski.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c2).Wait();
            userManager.AddToRoleAsync(c2, "user").Wait();

            var c3 = new Customer
            {
                Name = "Ksiadz Robak",
                Email = "ksiadz@robak.pl",
                NormalizedEmail = "ksiadz@robak.pl",
                UserName = "ksiadz@robak.pl",
                NormalizedUserName = "ksiadz@robak.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c3).Wait();
            userManager.AddToRoleAsync(c3, "storemanager").Wait();


            context.SaveChanges();

            /*
            context.Customers.AddRange(
                 new Customer
                 {
                     Name = "Jan Kowalski",
                     HasNewsletterSubscribed = false,
                     MembershipTypeId = 1,
                     Birthdate = new DateTime(1999, 07, 06)
                 },
                 new Customer
                 {
                     Name = "Ksiądz Robak",
                     HasNewsletterSubscribed = true,
                     MembershipTypeId = 2,
                     Birthdate = new DateTime(1999, 5, 13)
                 },
                 new Customer
                 {
                     Name = "Daj kamienia",
                     HasNewsletterSubscribed = true,
                     MembershipTypeId = 3,
                     Birthdate = new DateTime(2001, 7, 22)
                 }
             ); */
        }
    }
}