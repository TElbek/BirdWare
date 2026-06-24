using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    public class BedsteMaanedForFamilieController(IBedsteMaanedForFamilieQuery bedsteMaanedForFamilieQuery) : ControllerBase
    {
        private readonly IBedsteMaanedForFamilieQuery bedsteMaanedForFamilieQuery = bedsteMaanedForFamilieQuery;

        [HttpGet]
        [Route("api/BedsteMaanedForFamilie")]
        public List<BedsteMaanedForFamilie> BedsteMaanedForFamilie()
        {
            return bedsteMaanedForFamilieQuery.GetBedsteMaanedForFamilie();
        }
    }
}
