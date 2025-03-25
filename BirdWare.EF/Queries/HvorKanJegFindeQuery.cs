using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class HvorKanJegFindeQuery(BirdWareContext birdWareContext) : IHvorKanJegFindeQuery
    {
        public List<spHvorKanJegFindeResult> GetHvorKanJegFinde()
        {
            return [.. birdWareContext.SpHvorKanJegFindeResult.FromSqlRaw("spHvorKanJegFinde")];
        }
    }
}
