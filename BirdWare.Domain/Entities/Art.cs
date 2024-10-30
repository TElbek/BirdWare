using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Art
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long GruppeId { get; set; }
        [StringLength(30)]
        public string? Navn { get; set; }
        public bool SU { get; set; }
        public bool SetIDK { get; set; }

        public virtual Gruppe Gruppe { get; set; }
        public virtual ICollection<Observation> Observationer { get; set; }

        public Art()
        {
            Gruppe = new Gruppe();
            Observationer = [];
        }
    }
}
