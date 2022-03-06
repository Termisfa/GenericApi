using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GenericApi
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo GetProperty(this Type type, int index)
        {
            try
            {
                return type.GetProperties().FirstOrDefault(p => ((DisplayAttribute)p.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order == index);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
