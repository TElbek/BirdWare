using System.Text.Json.Serialization;

namespace BirdWare.Domain.Models
{
    public class Tag
    {
        [JsonPropertyName("tagType")]
        public TagTypes TagType { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("parentId")]
        public long ParentId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
   
    public enum TagTypes
    {
        Ukendt  = 0,
        Aarstal = 1,
        Maaned = 2,
        SaesonForaar = 3,
        SaesonSommer = 4,
        SaesonEfteraar = 5,
        SaesonVinter = 6,
        Lokalitet = 7,
        Region = 8,
        Land = 9,
        Familie = 10,
        Gruppe = 11,
        Art = 12
    }
}
