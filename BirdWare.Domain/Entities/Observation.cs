using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Observation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ArtId { get; set; }
        public long FugleturId { get; set; }
        public string? Beskrivelse { get; set; }

        public virtual Art Art { get; set; }
        public virtual Fugletur Fugletur { get; set; }

        public Observation()
        {
            Art = new Art();
            Fugletur = new Fugletur();
        }
    }
}
