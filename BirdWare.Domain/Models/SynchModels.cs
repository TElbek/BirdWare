using System.ComponentModel.DataAnnotations.Schema;

namespace BirdWare.Domain.Models
{
    [NotMapped]
    public class SynchLokalitet
    {
        public string LokalitetNavn { get; set; } = string.Empty;
        public long Regionid { get; set; }
        public long LokalitetId { get; set; }
    }

    [NotMapped]
    public class SynchObservation
    {
        public long ArtId { get; set; }
        public string Beskrivelse { get; set; } = string.Empty;
    }

    [NotMapped]
    public class SynchFugletur
    {
        public SynchFugletur()
        {
            Lokalitet = new SynchLokalitet();
            Observation = [];
        }

        public long FugleturId { get; set; }
        public long LokalitetId { get; set; }
        public DateTime Dato { get; set; }
        public SynchLokalitet Lokalitet { get; set; }
        public List<SynchObservation> Observation { get; set; }
    }

    [NotMapped]
    public class SynchTrip
    {
        public SynchTrip()
        {
            Fugletur = new SynchFugletur();
        }
        public SynchFugletur Fugletur { get; set; }
    }
}