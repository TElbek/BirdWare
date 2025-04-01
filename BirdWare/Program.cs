using BirdWare.Cache;
using BirdWare.EF;
using BirdWare.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.CodeAnalysis;

namespace BirdWare
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static readonly string corsPolicyName = "CorsPolicy";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicyName,
                        builder =>
                        {
                            builder.WithOrigins(["http://localhost:8080", "http://birdwareadm.home"])
                                    .AllowAnyHeader()
                                    .SetIsOriginAllowed((host) => true)
                                    .AllowAnyMethod();
                        });
            });

            builder.Services.AddControllers();
            builder.Services.AddDbContextFactory<BirdWareContext>(options => options.UseSqlServer(connString));
            builder.Services.AddTransient<IMemoryCache, MemoryCache>();
            builder.Services.AddSingleton<ITagMemoryCache, TagMemoryCache>();
            builder.Services.Register(builder.Configuration);

            var app = builder.Build();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(corsPolicyName);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run();
        }
    }
}