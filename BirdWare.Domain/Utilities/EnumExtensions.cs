using System.ComponentModel;
using System.Reflection;

namespace BirdWare.Domain.Utilities
{
    public static class EnumExtensions
    { 
        public static string GetDescription(this Enum value) 
        { 
            if (value == null) return string.Empty; 
            var fi = value.GetType().GetField(value.ToString()); 
            var attr = fi?.GetCustomAttribute<DescriptionAttribute>(); return attr?.Description ?? value.ToString(); 
        } 
    }
}

