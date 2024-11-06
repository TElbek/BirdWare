using BirdWare.EF.Interfaces;
using BirdWare.EF.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirdWare.EF
{
    public static class ServiceCatalog
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IArtQueries, ArtQueries>();
            services.AddTransient<IArterAarQueries, ArterAarQueries>();
            services.AddTransient<IForeslaaArterPaaTurQuery, ForeslaaArterPaaTurQuery>();
            services.AddTransient<IForskelQueries, ForskelQueries>();
            services.AddTransient<IFugleturAnalyseQuerySP, FugleturAnalyseQuerySP>();
            services.AddTransient<IFugletureByTagsQuery, FugletureByTagsQuery>();
            services.AddTransient<IFugleturQuery, FugleturQuery>();
            services.AddTransient<IFugleturObservationQuery, FugleturObservationQuery>();
            services.AddTransient<IObservationsByTagsQuery, ObservationsByTagsQuery>();
            services.AddTransient<ITagQuery, TagQuery>();
            services.AddTransient<IAaretsGangQuery, AaretsGangQuery>();
            return services;
        }
    }
}
