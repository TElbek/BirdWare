using BirdWare.Domain.Models;
using BirdWare.EF.Commands;
using BirdWare.EF.Interfaces;
using BirdWare.EF.Queries;
using BirdWare.EF.Security;
using BirdWare.EF.TagFilters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BirdWare.EF
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCatalog
    {
        public static IServiceCollection RegisterEF(this IServiceCollection services)
        {
            RegisterQueries(services);
            RegisterObservationTagFilters(services);
            RegisterFugleturTagFilters(services);

            RegisterCommands(services);
            RegisterLoginHelper(services);

            return services;
        }

        private static void RegisterQueries(IServiceCollection services)
        {
            services.AddTransient<IArtQueries, ArtQueries>();
            services.AddTransient<IArterAarQueries, ArterAarQueries>();

            services.AddTransient<IForeslaaArterPaaTurQuery, ForeslaaArterPaaTurQuery>();
            services.AddTransient<IForskelQueries, ForskelQueries>();
            services.AddTransient<IFugleturAnalyseQuerySP, FugleturAnalyseQuerySP>();
            services.AddTransient<IFugleturAnalyseQuery, FugleturAnalyseQuery>();
            services.AddTransient<IFugletureByTagsQuery, FugletureByTagsQuery>();
            services.AddTransient<IFugleturQuery, FugleturQuery>();
            services.AddTransient<IFugleturObservationQuery, FugleturObservationQuery>();

            services.AddTransient<IHvorKanJegFindeQuery, HvorKanJegFindeQuery>();
            services.AddTransient<ILokaliteterByLatLongQuerySP, LokaliteterByLatLongQuerySP>();

            services.AddTransient<IObservationsByTagsQuery, ObservationsByTagsQuery>();
            services.AddTransient<IObservationsByLatLongQuery, ObservationsByLatLongQuery>();
            services.AddTransient<ISynchTripQuery, SynchTripQuery>();

            services.AddTransient<ITagQuery, TagQuery>();
            services.AddTransient<IAaretsGangQuery, AaretsGangQuery>();
            services.AddTransient<IBrugerQuery, BrugerQuery>();
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddTransient<IOpdaterObsCommand, OpdaterObsCommand>();
            services.AddTransient<IOpretObsCommand, OpretObsCommand>();
            services.AddTransient<IOpretTurCommand, OpretTurCommand>();
            services.AddTransient<ISletObsCommand, SletObsCommand>();
            services.AddTransient<ISynchTripCommand, SynchTripCommand>();
        }

        private static void RegisterLoginHelper(IServiceCollection services)
        {
            services.AddTransient<ILoginHelper, LoginHelper>();
        }

        private static void RegisterFugleturTagFilters(IServiceCollection services)
        {
            services.AddKeyedTransient<IFugleturTagFilter, FugleturLandFilter>(TagTypes.Land);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturLokalitetFilter>(TagTypes.Lokalitet);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturRegionFilter>(TagTypes.Region);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturSaesonFilter>(TagTypes.SaesonVinter);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturSaesonFilter>(TagTypes.SaesonForaar);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturSaesonFilter>(TagTypes.SaesonSommer);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturSaesonFilter>(TagTypes.SaesonEfteraar);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturMaanedFilter>(TagTypes.Maaned);
            services.AddKeyedTransient<IFugleturTagFilter, FugleturAarstalFilter>(TagTypes.Aarstal);
        }

        private static void RegisterObservationTagFilters(IServiceCollection services)
        {
            services.AddKeyedTransient<IObservationTagFilter, ObservationArtFilter>(TagTypes.Art);
            services.AddKeyedTransient<IObservationTagFilter, ObservationGruppeFilter>(TagTypes.Gruppe);
            services.AddKeyedTransient<IObservationTagFilter, ObservationFamilieFilter>(TagTypes.Familie);
            services.AddKeyedTransient<IObservationTagFilter, ObservationLandFilter>(TagTypes.Land);
            services.AddKeyedTransient<IObservationTagFilter, ObservationLokalitetFilter>(TagTypes.Lokalitet);
            services.AddKeyedTransient<IObservationTagFilter, ObservationRegionFilter>(TagTypes.Region);
            services.AddKeyedTransient<IObservationTagFilter, ObservationSaesonFilter>(TagTypes.SaesonVinter);
            services.AddKeyedTransient<IObservationTagFilter, ObservationSaesonFilter>(TagTypes.SaesonForaar);
            services.AddKeyedTransient<IObservationTagFilter, ObservationSaesonFilter>(TagTypes.SaesonSommer);
            services.AddKeyedTransient<IObservationTagFilter, ObservationSaesonFilter>(TagTypes.SaesonEfteraar);
            services.AddKeyedTransient<IObservationTagFilter, ObservationMaanedFilter>(TagTypes.Maaned);
            services.AddKeyedTransient<IObservationTagFilter, ObservationAarstalFilter>(TagTypes.Aarstal);
        }
    }
}
