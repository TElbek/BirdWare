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

            return [.. (from sty in speciesThisYear
                    where !speciesLastYear.Any(t => t.ArtId == sty.ArtId)
                    join fty in birdWareContext.Fugletur on sty.FugleturId equals fty.Id
                    join lty in birdWareContext.Lokalitet on fty.LokalitetId equals lty.Id
                    join gr in birdWareContext.Gruppe on sty.GruppeId equals gr.Id
                    join fa in birdWareContext.Familie on gr.FamilieId equals fa.Id
                    select new Forskel
                    {
                        ArtId = sty.ArtId,
                        ArtNavn = sty.ArtNavn,
                        FamilieNavn = fa.Navn,
                        Dato = sty.Dato,
                        RegionId = lty.RegionId,
                        SU = sty.SU,
                        LokalitetId = lty.Id,
                        LokalitetNavn = lty.Navn
                    }).OrderByDescending(o => o.Dato)];
        }

        public List<Forskel> GetForskelSidsteAar()
        {
            var speciesThisYear = arterAarQueries.GetArterIAar();
            var speciesLastYear = arterAarQueries.GetArterSidsteAar();

            return [.. (from sly in speciesLastYear
                    where !speciesThisYear.Any(t => t.ArtId == sly.ArtId)
                    join fly in birdWareContext.Fugletur on sly.FugleturId equals fly.Id
                    join lly in birdWareContext.Lokalitet on fly.LokalitetId equals lly.Id
                    join gr in birdWareContext.Gruppe on sly.GruppeId equals gr.Id
                    join fa in birdWareContext.Familie on gr.FamilieId equals fa.Id
                    select new Forskel
                    {
                        ArtId = sly.ArtId,
                        ArtNavn = sly.ArtNavn,
                        FamilieNavn = fa.Navn,
                        Dato = sly.Dato,
                        RegionId = lly.RegionId,
                        SU = sly.SU,
                        LokalitetId = lly.Id,
                        LokalitetNavn = lly.Navn
                    }).OrderByDescending(o => o.Dato)];
        }
    }
}