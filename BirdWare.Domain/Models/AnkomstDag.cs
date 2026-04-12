namespace BirdWare.Domain.Models
{
    public class AnkomstDag
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public DateTime AnkomstDato { get; set; }
        public DateTime? SetIaarDato { get; set; }
        public bool ErSetIaar => SetIaarDato.HasValue;
        public bool SenereIaar => SetIaarDato.HasValue && AnkomstDato > SetIaarDato.Value;
        public double Forskel => (AnkomstDato - SetIaarDato.GetValueOrDefault()).TotalDays;
    }
}
