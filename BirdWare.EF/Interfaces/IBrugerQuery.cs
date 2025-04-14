using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IBrugerQuery
    {
        Bruger GetBrugerByName(string UserName);
    }
}
