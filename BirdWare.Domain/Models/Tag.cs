using BirdWare.Domain.Utilities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BirdWare.Domain.Models
{
    public class Tag
    {
        [JsonPropertyName("tagType")]
        public TagTypes TagType { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("someValue")]
        public long SomeValue { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        public string TypeName => TagType.GetDescription();
    }

    public enum TagTypes
    {
        [Description("")]
        Ukendt  = 0,
        [Description("Årstal")]
        Aarstal = 1,
        [Description("Måned")]
        Maaned = 2,
        [Description("Sæson")]
        SaesonForaar = 3,
        [Description("Sæson")]
        SaesonSommer = 4,
        [Description("Sæson")]
        SaesonEfteraar = 5,
        [Description("Sæson")]
        SaesonVinter = 6,
        [Description("Lokalitet")]
        Lokalitet = 7,
        [Description("Region")]
        Region = 8,
        [Description("Land")]
        Land = 9,
        [Description("Familie")]
        Familie = 10,
        [Description("Gruppe")]
        Gruppe = 11,
        [Description("Art")]
        Art = 12,
        [Description("Seneste år")]
        SenesteNÅr = 13,
        [Description("Halvår")]
        HalvårVinter = 14,
        [Description("Halvår")]
        HalvårSommer = 15,
        [Description("Distance")]
        Distance = 16,
        [Description("Kommune")]
        Kommune = 17,
        [Description("Træktid")]
        TrækTidForår = 18,
        [Description("Træktid")]
        TrækTidEfterår = 19,
        [Description("Træktid")]
        TrækTidUdenfor = 20,
    }
}
