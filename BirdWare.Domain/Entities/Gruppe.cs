using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BirdWare.Domain.Entities
{
    public class Gruppe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long FamilieId { get; set; }
        [StringLength(30)]
        public string? Navn { get; set; }

        public virtual ICollection<Art> Arter { get; set; }

        public virtual Familie Familie { get; set; }

        public Gruppe()
        {
            Arter = [];
            Familie = new Familie();
        }
    }
}
