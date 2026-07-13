using BirdWare.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Interfaces
{
    public interface ISoegArtIkkeSetPaaTurHandler
    {
        IEnumerable<Tag> Handle([FromQuery] string query);
    }
}
