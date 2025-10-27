using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;

namespace NextUse.DAL.Extensions
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // EF defaulter til cascade delete, og MSSQL kan ikke håndtere dette, når der er flere foreign keys til samme table (selvom f.eks. MySql og MariaDB kan).
            // Vi er derfor nødsaget til at bruge FluentAPI til at ændre på dette
            builder.Entity<Rating>()
              .HasOne(r => r.FromProfile)
              .WithMany()
              .HasForeignKey(r => r.FromProfileId)
              .OnDelete(DeleteBehavior.NoAction); // NOTE: Hvis Cascade ønskes, skal det gøres gennem koden

            builder.Entity<Profile>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Profile)
                .OnDelete(DeleteBehavior.NoAction);

            //  builder.Entity<Profile>()
            //.HasOne(p => p.User)
            //.WithOne()
            //.HasForeignKey<Profile>(p => p.UserId)
            //.OnDelete(DeleteBehavior.Cascade); // Ensures profile is deleted when user is deleted
            builder.Entity<Profile>()
              .HasOne(p => p.User)
              .WithOne(u => u.Profile)
              .HasForeignKey<Profile>(p => p.UserId)
              .IsRequired()
              .HasConstraintName("FK_Profile_User");

            builder.Entity<Profile>()
                .HasMany(p => p.Bookmarks)
                .WithOne(b => b.Profile)
                .HasForeignKey(b => b.ProfileId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Product>()
                .HasOne(r => r.Address)
                .WithOne(a => a.Product)
                .OnDelete(DeleteBehavior.NoAction); // NOTE: Cascade ønskes, skal det gøres gennem koden

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction); // NOTE: Cascade ønskes, skal det gøres gennem koden
                                                   // REMEMBER THIS SHIT WHEN WE WORK ON FRONT END 


            builder.Entity<Product>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(p => p.Bookmarks)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Change Delete behavior for Profile
            builder.Entity<Bookmark>()
                .HasOne(b => b.Profile)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.ProfileId)
                .OnDelete(DeleteBehavior.NoAction);  // Change from Cascade to  NoAction

            builder.Entity<Bookmark>()
                .HasOne(b => b.Product)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Change from Cascade to  NoAction

            builder.Entity<Comment>()
               .HasOne(c => c.Profile)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.ProfileId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
              .HasOne(c => c.Product)
              .WithMany(p => p.Comments)
              .HasForeignKey(c => c.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(m => m.FromProfile)
                .WithMany()
                .HasForeignKey(m => m.FromProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                .HasOne(m => m.ToProfile)
                .WithMany()
                .HasForeignKey(m => m.ToProfileId)
                .OnDelete(DeleteBehavior.NoAction);




            base.OnModelCreating(builder);

            builder.HasDefaultSchema("NextUse");

        }
    }
}
