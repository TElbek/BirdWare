using BirdWare.Cache;
using BirdWare.Domain;
using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.Domain.Security;
using BirdWare.EF;
using BirdWare.Interfaces;
using DotNetEnv;
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
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            var connString = Environment.GetEnvironmentVariable("BirdWareConn");

            if(string.IsNullOrEmpty(connString))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection string is not set.");
                Console.ResetColor();
            }

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
            builder.Services.RegisterEF(builder.Configuration);
            builder.Services.RegisterDomain(builder.Configuration);

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