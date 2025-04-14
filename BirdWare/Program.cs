using BirdWare.Cache;
using BirdWare.Domain;
using BirdWare.EF;
using BirdWare.Interfaces;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer =   "birdware.dk",
                    ValidAudience = "birdware.dk",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("appSecret") ?? string.Empty))
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddDbContextFactory<BirdWareContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("BirdWareConn")));
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