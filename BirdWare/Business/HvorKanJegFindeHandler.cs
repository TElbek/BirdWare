using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;

namespace BirdWare.Business
{
    public class HvorKanJegFindeHandler(IHvorKanJegFindeQuery hvorKanJegFindeQuery) : IHvorKanJegFindeHandler
    {
        private const int MinimumAntalObs = 2;

        public List<HvorKanJegFindeResponse> GetHvorKanJegFinde()
        {
            var nytaarsDagIAar = GetNytaarsDagIAar();
            var startDag = GetRelativDagIAaret(-30);
            var slutdag = GetRelativDagIAaret(30);

            var vsopPOI = hvorKanJegFindeQuery.GetVSOPPOI();
            var arterJegIkkeHarSetIAar = hvorKanJegFindeQuery.GetArterJegIkkeHarSetIAar(nytaarsDagIAar);
            var obsTidligereAar = hvorKanJegFindeQuery.GetObsTidligereAar(startDag, slutdag, vsopPOI, arterJegIkkeHarSetIAar);

            var hvorKanJegFindeList = new List<HvorKanJegFindeResponse>();

            foreach (var obs in obsTidligereAar)
            {
                var antalObsPaaLokalitet = obsTidligereAar.Count(q => q.ArtId == obs.ArtId && q.LokalitetId == obs.LokalitetId);
                var erFundet = hvorKanJegFindeList.Any(q => q.ArtId == obs.ArtId);

                if (erFundet)
                {
                    var eksisterende = hvorKanJegFindeList.Single(q => q.ArtId == obs.ArtId);
                    if (eksisterende != null && (
                        HarFlereObs(antalObsPaaLokalitet, eksisterende) ||
                        ErTaetterePaa(obs, antalObsPaaLokalitet, eksisterende)))
                    {
                        OpdaterHvorKanJegFinde(obs, antalObsPaaLokalitet, eksisterende);
                    }
                }
                else if (antalObsPaaLokalitet >= MinimumAntalObs)
                {
                    hvorKanJegFindeList.Add(HvorKanJegFindeFactory(obs, antalObsPaaLokalitet));
                }
            }
            return hvorKanJegFindeList;
        }

        private static void OpdaterHvorKanJegFinde(ObsTidligereAar item, int antalObs, HvorKanJegFindeResponse found)
        {
            found.LokalitetId = item.LokalitetId;
            found.LokalitetNavn = item.LokalitetNavn;
            found.Distance = item.Distance;
            found.AntalObs = antalObs;
        }

        private static HvorKanJegFindeResponse HvorKanJegFindeFactory(ObsTidligereAar item, int antalObs)
        {
            return new HvorKanJegFindeResponse
            {
                ArtId = item.ArtId,
                ArtNavn = item.ArtNavn,
                GruppeNavn = item.GruppeNavn,
                FamilieNavn = item.FamilieNavn,
                Distance = item.Distance,
                AntalObs = antalObs,
                LokalitetId = item.LokalitetId,
                LokalitetNavn = item.LokalitetNavn,
            };
        }

        private static int GetRelativDagIAaret(int days)
        {
            return DateTime.Now.AddDays(days).DayOfYear;
        }

        private static DateTime GetNytaarsDagIAar()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        private static bool ErTaetterePaa(ObsTidligereAar obs, int antalObs, HvorKanJegFindeResponse existing)
        {
            return existing.Distance > obs.Distance && antalObs >= MinimumAntalObs;
        }

        private static bool HarFlereObs(int antalObs, HvorKanJegFindeResponse existing)
        {
            return existing.AntalObs < antalObs;
        }
    }
}
