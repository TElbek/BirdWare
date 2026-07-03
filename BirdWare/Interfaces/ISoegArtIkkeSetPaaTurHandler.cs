using BirdWare.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Interfaces
{
    public interface ISoegArtIkkeSetPaaTurHandler
    {
        List<Tag> GetTags([FromQuery] string query);
    }
}
