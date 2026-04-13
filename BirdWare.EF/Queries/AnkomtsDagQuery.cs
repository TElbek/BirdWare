using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class AnkomtsDagQuery(
        IAnkomtsDagSubQuery ankomtsDagSubQuery,
        IAnkomstDagIAarQuery ankomstDagIAarQuery) : IAnkomtsDagQuery
    {
        private readonly IAnkomtsDagSubQuery ankomtsDagSubQuery = ankomtsDagSubQuery;
        private readonly IAnkomstDagIAarQuery ankomstDagIAarQuery = ankomstDagIAarQuery;

        public async Task<List<AnkomstDag>> GetAnkomtsDage(long familieId)
        {
            var ankomtsDage = new List<AnkomstDag>();

            var iAarQueryTask = ankomstDagIAarQuery.GetIAarQuery(familieId);
            var ankomstDagQueryTask = ankomtsDagSubQuery.GetAnkomstDagQuery(familieId);

            await Task.WhenAll(iAarQueryTask, ankomstDagQueryTask);

            var iAarQuery = iAarQueryTask.Result;
            var ankomstDagQuery = ankomstDagQueryTask.Result;

            ankomtsDage.AddRange(from item in ankomstDagQuery.Select(s => new { s.ArtId, s.ArtNavn, s.Speciel, s.SU }).Distinct()
                let ankomstDatoIAar = GetAnkomstDatoIAar(iAarQuery, item.ArtId)
                select new AnkomstDag
                {
                    ArtId = item.ArtId,
                    ArtNavn = item.ArtNavn,
                    Speciel = item.Speciel,
                    SU = item.SU,
                    AnkomstDato = new DateTime(DateTime.Now.Year, 1, 1).AddDays(GetAnkomstDatoGennemsnit(ankomstDagQuery, item.ArtId)),
                    SetIaarDato = ankomstDatoIAar.HasValue ? new DateTime(DateTime.Now.Year, 1, 1).AddDays(ankomstDatoIAar.Value) : null,
                });

            return [.. ankomtsDage.OrderBy(o => o.ArtNavn)];
        }

        private static double GetAnkomstDatoGennemsnit(List<AnkomstDagBeregning> ankomstDagQuery, long artId)
        {
            var match = ankomstDagQuery.Where(x => x.ArtId == artId);
            return match != null ? match.Average(a => a.AnkomstDag) - 1 : 0;
        }

        private static double? GetAnkomstDatoIAar(List<AnkomstDagBeregning> iAarQuery, long artId)
        {
            var match = iAarQuery.FirstOrDefault(x => x.ArtId == artId);
            return match != null && match.SetIaarDag.HasValue ? match.SetIaarDag.Value - 1 : (double?)null;
        }        
    }
}