using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BaseSystem.Reflection
{
    public static class ReflectionUtils
    {
        public static Type[] GetSubTypes(this Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => type.IsAssignableFrom(x)).ToArray();
        }
    }
}