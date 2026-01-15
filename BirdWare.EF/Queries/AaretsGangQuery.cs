using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class AaretsGangQuery(BirdWareContext birdWareContext, IArterAarQueries arterAarQueries) : ContextBase(birdWareContext), IAaretsGangQuery
    {
        public List<AaretsGang> GetAaretsGang()
        {
            var aaretsGangList =
                from species in arterAarQueries.GetArterIAar()
                join fugletur in birdWareContext.Fugletur.AsNoTracking() on species.FugleturId equals fugletur.Id
                join lokalitet in birdWareContext.Lokalitet.AsNoTracking() on fugletur.LokalitetId equals lokalitet.Id
                join art in birdWareContext.Art.AsNoTracking() on species.ArtId equals art.Id
                join gruppe in birdWareContext.Gruppe.AsNoTracking() on art.GruppeId equals gruppe.Id
                join familie in birdWareContext.Familie.AsNoTracking() on gruppe.FamilieId equals familie.Id
                select new AaretsGang
                {
                    ArtId = art.Id,
                    ArtNavn = art.Navn,
                    Dato = fugletur.Dato,
                    FamilieNavn = familie.Navn,
                    FugleturId = fugletur.Id,
                    LokalitetNavn = lokalitet.Navn,
                    SU = art.SU,
                    Speciel = art.Speciel
                };

            return [.. aaretsGangList.OrderByDescending(o => o.Dato)];
        }
    }
}
