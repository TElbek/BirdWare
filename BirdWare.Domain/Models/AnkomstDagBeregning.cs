namespace BirdWare.Domain.Models
{
    public class AnkomstDagBeregning
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = string.Empty;
        public int Aarstal { get; set; }
        public int AnkomstDag { get; set; }
        public int? SetIaarDag { get; set; }
        public bool SU { get; set; }
        public bool Speciel { get; set; }
    }
}
