using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class AnkomtsDagQuery(BirdWareContext birdWareContext) :  ContextBase(birdWareContext), IAnkomtsDagQuery
    {
        public List<AnkomstDag> GetAnkomtsDage(long familieId)
        {
            var ankomtsDage = new List<AnkomstDag>();

            var iAarQuery = GetIAarQuery(familieId);

            var ankomstDagQuery = GetAnkomstDagQuery(familieId);

            foreach (var item in ankomstDagQuery)
            {
                ankomtsDage.Add(new AnkomstDag
                {
                    ArtId = item.ArtId,
                    ArtNavn = item.ArtNavn,
                    AnkomstDato = item.AnkomstDato,
                    SetIaarDato = iAarQuery.FirstOrDefault(x => x.ArtId == item.ArtId)?.SetIaarDato
                });
            }

            return ankomtsDage;
        }

        private List<AnkomstDagBeregning> GetAnkomstDagQuery(long familieId)
        {
            return [.. (from obs in birdWareContext.Observation
                    join art in birdWareContext.Art on obs.ArtId equals art.Id
                    join grupper in birdWareContext.Gruppe on art.GruppeId equals grupper.Id
                    join fugletur in birdWareContext.Fugletur on obs.FugleturId equals fugletur.Id
                    where grupper.FamilieId == familieId && art.SU == false
                    group obs by new { art.Id, art.Navn, Dato = fugletur.Dato ?? DateTime.MinValue } into g
                    let dayOfYear = g.Min(o => g.Key.Dato.DayOfYear)
                    group new { g.Key.Id, g.Key.Navn, dayOfYear } by new { g.Key.Id, g.Key.Navn } into finalGroup
                    select new AnkomstDagBeregning
                    {
                        ArtId = finalGroup.Key.Id,
                        ArtNavn = finalGroup.Key.Navn,
                        AnkomstDato = new DateTime(2026, 1, 1).AddDays((int)finalGroup.Average(x => x.dayOfYear) - 1)
                    })];
        }

        private List<AnkomstDagBeregning> GetIAarQuery(long familieId)
        {
            return [.. (from obs in birdWareContext.Observation
                    join art in birdWareContext.Art on obs.ArtId equals art.Id
                    join grupper in birdWareContext.Gruppe on art.GruppeId equals grupper.Id
                    join fugletur in birdWareContext.Fugletur on obs.FugleturId equals fugletur.Id
                    where grupper.FamilieId == familieId &&
                          art.SU == false &&
                          fugletur.Dato.HasValue && fugletur.Dato.Value.Year == DateTime.Now.Year
                    group obs by new { art.Id, art.Navn } into g
                    select new AnkomstDagBeregning
                    {
                        ArtId = g.Key.Id,
                        ArtNavn = g.Key.Navn,
                        SetIaarDato = g.Min(o => o.Fugletur.Dato)
                    })];
        }
    }

    internal class AnkomstDagBeregning
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public DateTime AnkomstDato { get; set; }
        public DateTime? SetIaarDato { get; set; }
    }