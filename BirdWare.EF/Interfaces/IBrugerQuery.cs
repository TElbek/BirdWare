using BirdWare.Domain.Entities;

namespace BirdWare.EF.Interfaces
{
    public interface IBrugerQuery
    {
        Bruger GetBrugerByName(string UserName);
    }
}
