using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BirdWare.Domain.Entities
{
    public class Fugletur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long LokalitetId {  get; set; }
        public DateTime? Dato { get; set; }

        public virtual Lokalitet Lokalitet { get; set; }
        public virtual ICollection<Observation> Observationer { get; set; }

        [NotMapped]
        public long Aarstal => Dato.HasValue ? Dato.Value.Year : 0;
        [NotMapped]
        public long Maaned => Dato.HasValue ? Dato.Value.Month : 0;

        public Fugletur()
        {
            Lokalitet = new Lokalitet();
            Observationer = [];
        }
    }

    public class FugleturAarMaaned
    { 
        public long FugleturId { get; set; }
        public long Aarstal {  get; set; }
        public long Maaned {  get; set; }
    }

    public static class FugleturExtensions
    {
        public static IQueryable<FugleturAarMaaned> GetAarMaaned(this IQueryable<Fugletur> fugleture)
        { 
            return from f in fugleture
                   select new FugleturAarMaaned { 
                       FugleturId = f.Id,
                       Aarstal = (f.Dato.HasValue ? f.Dato.Value.Year : 0),
                       Maaned = (f.Dato.HasValue ? f.Dato.Value.Month : 0)
                   };
        }
    }
}
