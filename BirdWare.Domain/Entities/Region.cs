using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [StringLength(30)]
        public string? Navn { get; set; }
        public bool Indland { get; set; }

        public virtual ICollection<Lokalitet> Lokaliteter { get; set; }

        public Region()
        {
            Lokaliteter = [];
        }
    }
}
