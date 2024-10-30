using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Lokalitet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long RegionId { get; set; }
        [StringLength(30)]
        public string? Navn { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Fugletur> Fugleture { get; set; }

        public Lokalitet()
        {
            Region = new Region();
            Fugleture = [];
        }
    }
}
