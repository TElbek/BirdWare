namespace BirdWare.Domain.Models
{
    public class AnkomstDag
    {
        public long ArtId { get; set; }
        public string ArtNavn { get; set; } = empty;
        public DateTime GnsAnkomstDato { get; set; }
        public DateTime? SetIaarDato { get; set; }
        public long Maaned => GetMaaned();
        public bool ErSetIaar => GetErSetIaar();
        public bool TidligereIAar => SetTidligereIAar();
        public double Forskel => GetForskel();

        #region properties
        private static readonly string empty = string.Empty;
        #endregion
        #region methods
        private int GetMaaned() => SetIaarDato.HasValue ? SetIaarDato.Value.Month : GnsAnkomstDato.Month;

        private bool GetErSetIaar() => SetIaarDato.HasValue;

        private bool SetTidligereIAar() => SetIaarDato.HasValue && GnsAnkomstDato.Date >= SetIaarDato.Value.Date;

        private double GetForskel() => ErSetIaar ? Math.Round((GnsAnkomstDato - SetIaarDato.GetValueOrDefault()).TotalDays,1) : 0;

        #endregion
    }
}
