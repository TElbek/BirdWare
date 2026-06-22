namespace BirdWare.Domain.Models
{
    public class TagGroup
    {
        public string Name { get; set; } = string.Empty;
        public List<Tag> Tags { get; set; } = [];
    }
}
