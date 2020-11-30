using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGen.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsPrimitiveOrString(this Type type) => type.IsPrimitive || type.IsValueType || type == typeof(string);
        public static bool IsCollection(this Type type)
        {
            var isCollection = type.GetInterface(nameof(IEnumerable)) != null ||
                               type.GetInterface(nameof(ICollection)) != null;

            return isCollection && !type.IsPrimitiveOrString();
        }
    }
}
