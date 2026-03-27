using BirdWare.Domain.Entities;

namespace BirdWare.EF.Interfaces
{
    public interface IOpretLokalitetCommand
    {
        bool OpretLokalitet(Lokalitet lokalitet);
    }
}
