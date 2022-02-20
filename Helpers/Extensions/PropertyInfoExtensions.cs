using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GenericApi
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo GetProperty(this Type type, int index)
        {
            return type.GetProperties().FirstOrDefault(p => ((DisplayAttribute)p.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order == index);
        }
    }
}
