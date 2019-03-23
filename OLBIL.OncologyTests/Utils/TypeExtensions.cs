using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OLBIL.OncologyTests.Utils
{
    public static class TypeExtensions
    {
        public static readonly Type[] EmptyTypes = new Type[0];

        static readonly Type ReadOnlyCollectionType = Type.GetType("System.Collections.Generic.IReadOnlyCollection`1", false);

        static readonly Type ReadOnlyListType = Type.GetType("System.Collections.Generic.IReadOnlyList`1", false);

        public static bool IsClosedTypeOf(this Type @this, Type openGeneric)
        {
            if (@this == null) throw new ArgumentNullException("this");
            if (openGeneric == null) throw new ArgumentNullException("openGeneric");

            if (!(openGeneric.IsGenericTypeDefinition || openGeneric.ContainsGenericParameters))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type '{0}' is not an open generic class or interface type.", openGeneric.FullName));

            return @this.GetTypesThatClose(openGeneric).Any();
        }

        public static bool IsAssignableTo<T>(this Type @this)
        {
            if (@this == null) throw new ArgumentNullException("this");
            return typeof(T).IsAssignableFrom(@this);
        }

        public static IEnumerable<Type> GetTypesThatClose(this Type @this, Type openGeneric)
        {
            return FindAssignableTypesThatClose(@this, openGeneric);
        }

        static IEnumerable<Type> FindAssignableTypesThatClose(Type candidateType, Type openGenericServiceType)
        {
            return TypesAssignableFrom(candidateType)
                .Where(t => IsGenericTypeDefinedBy(t, openGenericServiceType));
        }

        static IEnumerable<Type> TypesAssignableFrom(Type candidateType)
        {
            return candidateType.GetInterfaces().Concat(
                TraverseAcross(candidateType, t => t.BaseType));
        }

        public static IEnumerable<T> TraverseAcross<T>(T first, Func<T, T> next)
            where T : class
        {
            var item = first;
            while (item != null)
            {
                yield return item;
                item = next(item);
            }
        }

        public static bool IsGenericTypeDefinedBy(this Type @this, Type openGeneric)
        {
            if (@this == null) throw new ArgumentNullException("this");
            if (openGeneric == null) throw new ArgumentNullException("openGeneric");

            return !@this.ContainsGenericParameters && @this.IsGenericType && @this.GetGenericTypeDefinition() == openGeneric;
        }
    }
}
