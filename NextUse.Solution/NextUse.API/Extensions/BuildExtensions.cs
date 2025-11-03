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

            var userManager = scope.ServiceProvider.GetRequiredService <UserManager<User>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            if (!dbContext.Addresses.Any())
            {
                dbContext.Addresses.AddRange(
                    new Address
                    {
                        Country = "AdminCountry",
                        City = "AdminCity",
                        PostalCode = 1000,
                        Street = "AdminStreet",
                        HouseNumber = "1000"
                    },
                    new Address
                    {
                        Country = "ProfileCountry1",
                        City = "ProfileCity1",
                        PostalCode = 0001,
                        Street = "ProfileStreet1",
                        HouseNumber = "1A"
                    },
                    new Address
                    {
                        Country = "ProfileCountry2",
                        City = "ProfileCity2",
                        PostalCode = 0002,
                        Street = "ProfileStreet2",
                        HouseNumber = "2A"
                    },
                    new Address
                    {
                        Country = "ProductCountry1",
                        City = "ProductCity1",
                        PostalCode = 0001,
                        Street = "ProductStreet1",
                        HouseNumber = "1A"
                    },
                    new Address
                    {
                        Country = "ProductCountry2",
                        City = "ProductCity2",
                        PostalCode = 0002,
                        Street = "ProductStreet2",
                        HouseNumber = "2A"
                    });

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

            User? user1 = userManager.FindByEmailAsync("user1@nextuse.com").GetAwaiter().GetResult();
            if (user1 is null)
            {
                user1 = new User { UserName = "user1@nextuse.com", Email = "user1@nextuse.com" };
                userManager.CreateAsync(user1, "User123!").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user1, new Claim("level", "user")).GetAwaiter().GetResult();

                user1 = userManager.FindByEmailAsync("user1@nextuse.com").GetAwaiter().GetResult();

                dbContext.Profiles.Add(new Profile
                {
                    Name = "Profile1",
                    UserId = user1!.Id,
                    AddressId = 2
                });

                dbContext.SaveChanges();
            }

            User? user2 = userManager.FindByEmailAsync("user2@nextuse.com").GetAwaiter().GetResult();
            if (user2 is null)
            {
                user2 = new User { UserName = "user2@nextuse.com", Email = "user2@nextuse.com" };
                userManager.CreateAsync(user2, "User123!").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user2, new Claim("level", "user")).GetAwaiter().GetResult();

                user1 = userManager.FindByEmailAsync("user2@nextuse.com").GetAwaiter().GetResult();

                dbContext.Profiles.Add(new Profile
                {
                    Name = "Profile2",
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
                        Name = "Category1"
                    },
                    new Category
                    {
                        Name = "Category2"
                    });

                dbContext.SaveChanges();
            }

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(
                    new Product
                    {
                        Title = "ProductTitle1",
                        Description = "ProductDescription1",
                        Price = 100.00m,
                        ProfileId = 2,
                        CategoryId = 1,
                        AddressId = 4,
                    },
                    new Product
                    {
                        Title = "ProductTitle2",
                        Description = "ProductDescription2",
                        Price = 200.00m,
                        ProfileId = 3,
                        CategoryId = 2,
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
                        Content = "Comment For Product 1 From Profile 2",
                        ProductId = 1,
                        ProfileId = 3,
                    },
                    new Comment
                    {
                        Content = "Comment For Product 2 From Profile 1",
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
                        Content = "Message From Profile 1 To Profile 2",
                        FromProfileId = 2,
                        ToProfileId = 3,
                    },
                    new Message
                    {
                        Content = "Message From Profile 2 To Profile 1",
                        FromProfileId = 3,
                        ToProfileId = 2,
                    });

                dbContext.SaveChanges();
            }


        }
    }
}
