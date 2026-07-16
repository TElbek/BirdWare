using System.Reflection;

namespace BirdWare.Domain.Utilities
{
    public class StringOperations
    {
        public static string ToTitleCase(string value) => CultureProvider.GetCulture().TextInfo.ToTitleCase(value);

        public static string ClassAndMethodName(Type type, MethodBase method) => $"{type.Name}.{method.Name}";
    }
}
