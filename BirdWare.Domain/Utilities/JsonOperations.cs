using System.Text.Json;

namespace BirdWare.Domain.Utilities
{
    public static class JsonOperations<T> where T : class
    {
        public static List<T>? GetListFromJSON(string json) => 
            JsonSerializer.Deserialize<List<T>>(json);
    }
}
