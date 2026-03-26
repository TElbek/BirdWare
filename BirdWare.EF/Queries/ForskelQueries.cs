using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class ForskelQueries(BirdWareContext birdWareContext, IArterAarQueries arterAarQueries) : ContextBase(birdWareContext), IForskelQueries
    {
        public List<Forskel> GetForskelIAar()
        {
            var speciesThisYear = arterAarQueries.GetArterIAar();
            var speciesLastYear = arterAarQueries.GetArterSidsteAar();

            return Populate(speciesThisYear, speciesLastYear);
        }

        public List<Forskel> GetForskelSidsteAar()
        {
            var speciesThisYear = arterAarQueries.GetArterIAar();
            var speciesLastYear = arterAarQueries.GetArterSidsteAar();

            return Populate(speciesLastYear, speciesThisYear);
        }

        private List<Forskel> Populate(IQueryable<ArterAar> currentYear, IQueryable<ArterAar> compareYear)
        {
            return [.. (from cy in currentYear
                    where compareYear.All(t => t.ArtId != cy.ArtId)
                    join fcy in birdWareContext.Fugletur on cy.FugleturId equals fcy.Id
                    join lcy in birdWareContext.Lokalitet on fcy.LokalitetId equals lcy.Id
                    join gr in birdWareContext.Gruppe on cy.GruppeId equals gr.Id
                    join fa in birdWareContext.Familie on gr.FamilieId equals fa.Id
                    select new Forskel
                    {
                        ArtId = cy.ArtId,
                        ArtNavn = cy.ArtNavn,
                        FamilieNavn = fa.Navn,
                        Dato = cy.Dato,
                        RegionId = lcy.RegionId,
                        SU = cy.SU,
                        Speciel = cy.Speciel,
                        LokalitetId = lcy.Id,
                        LokalitetNavn = lcy.Navn
                    }).OrderByDescending(o => o.Dato)];
        }
    }
}