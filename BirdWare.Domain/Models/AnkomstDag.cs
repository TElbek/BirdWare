namespace BirdWare.Domain.Models
{
    public class AnkomstDag
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = empty;
        public bool Speciel { get; set; }
        public bool SU { get; set; }
        public DateTime AnkomstDato { get; set; }
        public DateTime? SetIaarDato { get; set; }
        public long Maaned => GetMaaned();
        public bool ErSetIaar => GetErSetIaar();
        public bool TidligereIAar => GetTidligereIAar();
        public double Forskel => GetForskel();

        #region properties
        private static readonly string empty = string.Empty;
        #endregion
        #region methods
        private int GetMaaned()
        {
            return SetIaarDato.HasValue ? SetIaarDato.Value.Month : AnkomstDato.Month;
        }

        private bool GetErSetIaar()
        {
            return SetIaarDato.HasValue;
        }

        private bool GetTidligereIAar()
        {
            return SetIaarDato.HasValue && AnkomstDato > SetIaarDato.Value;
        }

        private double GetForskel()
        {
            return ErSetIaar ? Math.Round((AnkomstDato - SetIaarDato.GetValueOrDefault()).TotalDays) : 0;
        }

        #endregion
    }
}
