using BirdWare.Domain.Models;
using NetTopologySuite.Geometries;

namespace BirdWare.EF.Interfaces
{
    public interface IHvorKanJegFindeQuery
    {
        IQueryable<long> GetArterJegIkkeHarSetIAar(DateTime nytaarsDagIAar);
        List<ObsTidligereAar> GetObsTidligereAar(int startDag, int slutdag, Point vsopPOI, IQueryable<long> arterJegIkkeHarSetIAar);
        Point GetVSOPPOI();
    }
}
