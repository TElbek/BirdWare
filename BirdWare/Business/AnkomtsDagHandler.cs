using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.Business
{
    public class AnkomtsDagHandler(IAnkomtsDagQuery ankomtsDagQuery) : IAnkomtsDagHandler
    {
        public IEnumerable<AnkomstDag> Handle(long familieId)
        {
            var ankomstArtLookup = ankomtsDagQuery.GetAnkomstData(familieId);

            foreach (var ankomstDageArt in ankomstArtLookup)
            {
                var ankomstDagIaarForArt = GetAnkomstDagIAar(ankomstDageArt);

                yield return new AnkomstDag
                {
                    ArtId = ankomstDageArt.Key,
                    ArtNavn = GetArtNavn(ankomstDageArt),
                    GnsAnkomstDato = GetGnsAnkomstDato(ankomstDageArt),
                    SetIaarDato = GetSetIaarDato(ankomstDagIaarForArt),
                };
            }
        }

        private static DateTime? GetSetIaarDato(double? iAarDag) => 
            iAarDag != null && iAarDag.HasValue ? new DateTime(DateTime.Now.Year, 1, 1).AddDays(iAarDag.Value - 1) : null;

        private static DateTime GetGnsAnkomstDato(IEnumerable<AnkomstDagBasis> ankomstDageArt) => 
            new DateTime(DateTime.Now.Year, 1, 1).AddDays(GetAnkomstDatoGennemsnit(ankomstDageArt));

        private static string GetArtNavn(IEnumerable<AnkomstDagBasis> ankomstDageArt) => 
            ankomstDageArt.FirstOrDefault()?.ArtNavn ?? string.Empty;

        private static double? GetAnkomstDagIAar(IEnumerable<AnkomstDagBasis> ankomstDageArt) => 
            ankomstDageArt.FirstOrDefault(x => x.Aarstal == DateTime.Now.Year)?.AnkomstDag;

        private static double GetAnkomstDatoGennemsnit(IEnumerable<AnkomstDagBasis> ankomstDageArt) => 
            ankomstDageArt.Any() ? ankomstDageArt.Average(a => a.AnkomstDag) - 1 : 0;
    }
}