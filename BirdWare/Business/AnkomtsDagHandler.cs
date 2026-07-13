using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.Business
{
    public class AnkomtsDagHandler(IAnkomtsDagQuery ankomtsDagQuery) : IAnkomtsDagHandler
    {
        public IEnumerable<AnkomstDag> Handle(long familieId)
        {
            var ankomstArtLookup = ankomtsDagQuery.GetAnkomstFamilie(familieId);

            foreach (var artId in ankomstArtLookup.Select(x => x.Key))
            {
                var ankomstDageArt = ankomstArtLookup[artId];
                var ankomstDagIaarForArt = GetAnkomstDagIAar(ankomstDageArt);

                yield return new AnkomstDag
                {
                    ArtId = artId,
                    ArtNavn = GetArtNavn(ankomstDageArt),
                    GnsAnkomstDato = GetGnsAnkomstDato(ankomstDageArt),
                    SetIaarDato = GetSetIaarDato(ankomstDagIaarForArt),
                };
            }
        }

        private static DateTime? GetSetIaarDato(double? iAarDag) => 
            iAarDag != null && iAarDag.HasValue ? new DateTime(DateTime.Now.Year, 1, 1).AddDays(iAarDag.Value) : null;

        private static DateTime GetGnsAnkomstDato(IEnumerable<AnkomstDagBeregning> ankomstDageArt) => 
            new DateTime(DateTime.Now.Year, 1, 1).AddDays(GetAnkomstDatoGennemsnit(ankomstDageArt));

        private static string GetArtNavn(IEnumerable<AnkomstDagBeregning> ankomstDageArt) => 
            ankomstDageArt.FirstOrDefault()?.ArtNavn ?? string.Empty;

        private static double? GetAnkomstDagIAar(IEnumerable<AnkomstDagBeregning> ankomstDageArt) => 
            ankomstDageArt.FirstOrDefault(x => x.Aarstal == DateTime.Now.Year)?.AnkomstDag;

        private static double GetAnkomstDatoGennemsnit(IEnumerable<AnkomstDagBeregning> ankomstDageArt) => 
            ankomstDageArt.Any() ? ankomstDageArt.Average(a => a.AnkomstDag) - 1 : 0;
    }
}