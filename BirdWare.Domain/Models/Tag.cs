using System.Text.Json.Serialization;

namespace BirdWare.Domain.Models
{
    public class Tag
    {
        [JsonPropertyName("tagType")]
        public TagTypes TagType { get; set; }
        public string TagTypeTitle { get; set; } = string.Empty;
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

    public static class TagTypeInfo
    {
        public static string GetTagTypeTitle(TagTypes type)
        {
            return type switch
            {
                TagTypes.Ukendt => string.Empty,
                TagTypes.Aarstal or TagTypes.Maaned => "Tidspunkt",
                TagTypes.Art => "Arter",
                TagTypes.Gruppe => "Gruppe",
                TagTypes.Familie => "Familie",
                TagTypes.Lokalitet or TagTypes.Region or TagTypes.Land => "Geografi",
                TagTypes.SaesonForaar or TagTypes.SaesonSommer or TagTypes.SaesonEfteraar or TagTypes.SaesonVinter => "Sæson",
                _ => "Ukendt",
            };
        }
    }
}
