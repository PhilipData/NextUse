using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using NextUse.API.Extensions;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository;
using NextUse.DAL.Repository.Interface;
using NextUse.Services.Services;
using NextUse.Services.Services.Interface;



namespace NextUse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder => builder.WithOrigins("http://localhost:4200") // Allow Angular frontend
                                      .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
                                      .AllowAnyHeader() // Allow all headers
                                      .AllowCredentials()); // Allow credentials if needed
            });

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            builder.Services.AddScoped<IBookmarkService, BookmarkService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddAuthorization(options =>
            {
                // Sætter roller og "levels" af adgang op
                options.AddPolicy("user", pb => pb
                    .RequireClaim("level", "user", "support", "admin"));

                options.AddPolicy("support", pb => pb
                    .RequireClaim("level", "support", "admin"));

                options.AddPolicy("admin", pb => pb
                    .RequireClaim("level", "admin"));
            });


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
     .AddEntityFrameworkStores<ApplicationDBContext>()
     .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None; // Allow cross-origin cookies
                options.Cookie.HttpOnly = true;
            });

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
            var app = builder.Build();
            app.UseCors("AllowAngularApp");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyMigrations();
                app.SeedDatabase();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}
