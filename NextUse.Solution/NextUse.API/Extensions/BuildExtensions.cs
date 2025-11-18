using Microsoft.AspNetCore.Identity;
using NextUse.DAL.Extensions;
using NextUse.DAL.Database.Entities;
using System.Security.Claims;

namespace NextUse.API.Extensions
{
    public static class BuildExtensions
    {
        public static void SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            if (!dbContext.Addresses.Any())
            {
                dbContext.Addresses.AddRange(
                   new Address
                   {
                       Country = "Danmark",
                       City = "København",
                       PostalCode = 2000,
                       Street = "Smallegade",
                       HouseNumber = "2B"
                   },
                   new Address
                   {
                       Country = "Danmark",
                       City = "Aarhus",
                       PostalCode = 8000,
                       Street = "Frederiks Allé",
                       HouseNumber = "15"
                   },
                   new Address
                   {
                       Country = "Danmark",
                       City = "Odense",
                       PostalCode = 5000,
                       Street = "Vesterbro",
                       HouseNumber = "33"
                   },
                   new Address
                   {
                       Country = "Danmark",
                       City = "Aalborg",
                       PostalCode = 9000,
                       Street = "Hasserisvej",
                       HouseNumber = "10A"
                   },
                   new Address
                   {
                       Country = "Danmark",
                       City = "Roskilde",
                       PostalCode = 4000,
                       Street = "Algade",
                       HouseNumber = "7"
                   }
                );


                dbContext.SaveChanges();
            }

            User? admin = userManager.FindByEmailAsync("admin@nextuse.com").GetAwaiter().GetResult();
            if (admin is null)
            {
                admin = new User { UserName = "admin@nextuse.com", Email = "admin@nextuse.com" };
                var test = userManager.CreateAsync(admin, "Admin123!").GetAwaiter().GetResult();
                var test2 = userManager.AddClaimAsync(admin, new Claim("level", "admin")).GetAwaiter().GetResult();

                admin = userManager.FindByEmailAsync("admin@nextuse.com").GetAwaiter().GetResult();

                dbContext.Profiles.Add(new Profile
                {
                    Name = "Admin",
                    UserId = admin!.Id,
                    AddressId = 1
                });

                dbContext.SaveChanges();
            }

            User? user1 = userManager.FindByEmailAsync("meharzain243@yahoo.com").GetAwaiter().GetResult();
            if (user1 is null)
            {
                user1 = new User { UserName = "meharzain243@yahoo.com", Email = "meharzain243@yahoo.com" };
                userManager.CreateAsync(user1, "User123!").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user1, new Claim("level", "user")).GetAwaiter().GetResult();

                user1 = userManager.FindByEmailAsync("meharzain243@yahoo.com").GetAwaiter().GetResult();

                dbContext.Profiles.Add(new Profile
                {
                    Name = "Zain",
                    UserId = user1!.Id,
                    AddressId = 2
                });

                dbContext.SaveChanges();
            }

            User? user2 = userManager.FindByEmailAsync("silentbird@nextuse.com").GetAwaiter().GetResult();
            if (user2 is null)
            {
                user2 = new User { UserName = "silentbird@nextuse.com", Email = "silentbird@nextuse.com" };
                userManager.CreateAsync(user2, "User123!").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user2, new Claim("level", "user")).GetAwaiter().GetResult();

                user1 = userManager.FindByEmailAsync("silentbird@nextuse.com").GetAwaiter().GetResult();

                dbContext.Profiles.Add(new Profile
                {
                    Name = "Philip",
                    UserId = user2!.Id,
                    AddressId = 3
                });

                dbContext.SaveChanges();
            }

            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(
                    new Category
                    {
                        Name = "Køkken"
                    },
                    new Category
                    {
                        Name = "Tøj"
                    },
                    new Category
                    {
                        Name = "Elektronik"
                    },
                    new Category
                    {
                        Name = "Møbler"
                    }
                    );

                dbContext.SaveChanges();
            }

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(
                    new Product
                    {
                        Title = "Stol",
                        Description = "Armchair",
                        Price = 100.00m,
                        ProfileId = 2,
                        CategoryId = 4,
                        AddressId = 4,
                    },
                    new Product
                    {
                        Title = "Bestik",
                        Description = "Mixed cutlery",
                        Price = 200.00m,
                        ProfileId = 3,
                        CategoryId = 1,
                        AddressId = 5,
                    });

                dbContext.SaveChanges();
            }

            if (!dbContext.Images.Any())
            {
                dbContext.Images.AddRange(
                    new Image
                    {
                        Blob = File.ReadAllBytes("TestData/product1-image.png"),
                        ProductId = 1
                    },
                    new Image
                    {
                        Blob = File.ReadAllBytes("TestData/product2-image.png"),
                        ProductId = 2
                    });

                dbContext.SaveChanges();
            }

            if (!dbContext.Ratings.Any())
            {
                dbContext.Ratings.AddRange(
                    new Rating
                    {
                        Score = 1,
                        FromProfileId = 2,
                        ToProfileId = 3,
                    },
                    new Rating
                    {
                        Score = 2,
                        FromProfileId = 3,
                        ToProfileId = 2,
                    });

                dbContext.SaveChanges();
            }

            if (!dbContext.Comments.Any())
            {
                dbContext.Comments.AddRange(
                    new Comment
                    {
                        Content = "Er det ægte læder?",
                        ProductId = 1,
                        ProfileId = 3,
                    },
                    new Comment
                    {
                        Content = "Dit produkt stinker og du burde ikke sælge sådan noget gammel lort!",
                        ProductId = 2,
                        ProfileId = 2,
                    });

                dbContext.SaveChanges();
            }

            if (!dbContext.Messages.Any())
            {
                dbContext.Messages.AddRange(
                    new Message
                    {
                        Content = "Sorry for the rude Comment",
                        FromProfileId = 2,
                        ToProfileId = 3,
                    },
                    new Message
                    {
                        Content = "F*cuk Dig",
                        FromProfileId = 3,
                        ToProfileId = 2,
                    });

                dbContext.SaveChanges();
            }


        }
    }
}
