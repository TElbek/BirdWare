namespace BirdWare.Domain.Models
{
    public static class SaesonMaaneder
    {
        public static Dictionary<TagTypes, List<long>> Liste { get; set; } = new Dictionary<TagTypes, List<long>>
        {
            { TagTypes.SaesonForaar, [3, 4, 5] },
            { TagTypes.SaesonSommer, [6, 7, 8] },
            { TagTypes.SaesonEfteraar, [9, 10, 11] },
            { TagTypes.SaesonVinter, [12, 1, 2] }
        };
    }
}
