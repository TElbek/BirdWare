using System.Text.Json;

namespace BirdWare.Domain.Utilities
{
    public static class JsonOperations<T> where T : class
    {
        public static List<T>? GetListFromJSON(string json)
        {
			try
			{
                return JsonSerializer.Deserialize<List<T>>(json);
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message + ", " + json);
                return [];
			}
        }
    }
}
