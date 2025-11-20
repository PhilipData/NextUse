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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Rating>()
              .HasOne(r => r.FromProfile)
              .WithMany()
              .HasForeignKey(r => r.FromProfileId)
              .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<Profile>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Profile)
                .OnDelete(DeleteBehavior.NoAction);

            //  builder.Entity<Profile>()
            //.HasOne(p => p.User)
            //.WithOne()
            //.HasForeignKey<Profile>(p => p.UserId)
            //.OnDelete(DeleteBehavior.Cascade); 
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
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction); 


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

            builder.Entity<Bookmark>()
                .HasOne(b => b.Profile)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.ProfileId)
                .OnDelete(DeleteBehavior.NoAction);  

            builder.Entity<Bookmark>()
                .HasOne(b => b.Product)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

          

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


            builder.Entity<Cart>()
                .ToTable("Carts")
                .HasOne(c => c.Profile)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Cart>()
                .HasIndex(c => new { c.ProfileId, c.Status })
                .IsUnique()
                .HasFilter("[ProfileId] IS NOT NULL AND [Status] = 'Active'");
            
            builder.Entity<Cart>()
                .HasIndex(c => new { c.AnonymousId, c.Status })
                .IsUnique()
                .HasFilter("[AnonymousId] IS NOT NULL AND [Status] = 'Active'");




            base.OnModelCreating(builder);

            builder.HasDefaultSchema("NextUse");

        }
    }
}
