using System;
using System.Linq;

namespace CourseCatalog.App.Helpers
{
    public static class AssemblyExtensions
    {
        public static T GetAssemblyAttribute<T>(this System.Reflection.Assembly ass) where T : Attribute
        {
            var attributes = ass.GetCustomAttributes(typeof(T), false);
            return attributes.Length != 0 ? attributes.OfType<T>().SingleOrDefault() : null;
        }

    }
}
