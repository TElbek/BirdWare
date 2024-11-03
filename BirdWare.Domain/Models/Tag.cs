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
        Aarstal,
        Maaned,
        SaesonForaar,
        SaesonSommer,
        SaesonEfteraar,
        SaesonVinter,
        Lokalitet,
        Region,
        Land,
        Familie,
        Gruppe,
        Art
    }
}
