using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IHvorKanJegFindeQuery
    {
        List<spHvorKanJegFindeResult> GetHvorKanJegFinde();
    }
}
