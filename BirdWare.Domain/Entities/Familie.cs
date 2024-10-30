using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Familie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [StringLength(30)]
        public string? Navn { get; set; }

        public virtual ICollection<Gruppe> Grupper { get; set; }

        public Familie()
        {
            Grupper = [];
        }
    }
}
