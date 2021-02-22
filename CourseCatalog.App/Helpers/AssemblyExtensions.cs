using System;
using System.Linq;

namespace CourseCatalog.App.Helpers
{
    public static class AssemblyExtensions
    {
        public static T GetAssemblyAttribute<T>(this System.Reflection.Assembly ass) where T : Attribute
        {
            object[] attributes = ass.GetCustomAttributes(typeof(T), false);
            if (attributes == null || attributes.Length == 0)
                return null;
            return attributes.OfType<T>().SingleOrDefault();
        }

    }
}
