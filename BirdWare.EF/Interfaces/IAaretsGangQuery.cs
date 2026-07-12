using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IAaretsGangQuery
    {
        IEnumerable<AaretsGang> GetAaretsGang();
    }
}
