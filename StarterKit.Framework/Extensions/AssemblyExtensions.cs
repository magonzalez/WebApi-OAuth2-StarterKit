using System.Linq;
using System.Reflection;

namespace StarterKit.Framework.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetBuildLabel()
        {
            var descriptionAttribute = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                .OfType<AssemblyDescriptionAttribute>()
                .FirstOrDefault();

            return descriptionAttribute != null
                ? descriptionAttribute.Description
                : "Local Developer Build";
        } 
    }
}
