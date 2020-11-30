using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SignalGen.Extensions;
using SignalGen.Interfaces;
using SignalGen.Models;

namespace SignalGen
{
    internal class TypeMapper: ITypeMapper
    {
        public List<TypeDescription> CustomTypes { get; } = new List<TypeDescription>();
        public TypeDescription MapType(Type type, string propertyName)
        {
            if (type.IsPrimitiveOrString())
            {
                return new TypeDescription
                {
                    PropertyName = propertyName.ToCamelCase(),
                    TypeName = type.Name,
                    IsCollection = false
                };
            }

            if (type.IsCollection())
            {
                var collectionGenericType = MapCollectionType(propertyName, type);
                return new TypeDescription
                {
                    PropertyName = propertyName.ToCamelCase(),
                    TypeName = collectionGenericType.Type,
                    IsCollection = true
                };
            }

            var customType = new TypeDescription
            {
                PropertyName = propertyName.ToCamelCase(),
                TypeName = type.Name,
                Fields = type
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Select((p) => MapType(p.PropertyType, p.Name))
                    .ToList()
            };

            var typeIsAlreadyMapped = CustomTypes.Any(t => t.TypeName == type.Name);

            if (!typeIsAlreadyMapped)
            {
                CustomTypes.Add(customType);
            }
            return customType;

        }

        public ArgumentInfo MapCollectionType(string name, Type collectionType)
        {
            var genericArguments = collectionType.GetGenericArguments();

            var collectionGenericType = genericArguments.FirstOrDefault();

            if (collectionGenericType == null)
                return null;

            return new ArgumentInfo
            {
                Name = name.ToCamelCase(),
                Type = collectionGenericType.Name,
                IsCollection = true
            };
        }
    }
}
