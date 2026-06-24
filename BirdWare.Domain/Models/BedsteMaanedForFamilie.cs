namespace BirdWare.Domain.Models
{
    public class BedsteMaanedForFamilie(int maaned, string familieNavn, int antalArter)
    {
        public long Maaned { get; set; } = maaned;
        public string FamilieNavn { get; set; } = familieNavn;
        public long AntalArter { get; set; } = antalArter;
    }
}
