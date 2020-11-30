using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalGen.Extensions;
using SignalGen.Interfaces;
using SignalGen.Models;

namespace SignalGen
{
    internal class HubManager: IHubManager
    {
        private readonly ITypeMapper _typeMapper;
        private readonly Dictionary<string, string> _hubRouteMap;

        public HubManager(ITypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
            _hubRouteMap = new Dictionary<string, string>();
        }

        public IEnumerable<Type> GetHubClasses() => Assembly.GetEntryAssembly().GetTypes().Where(t =>
            t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Hub)) || t.IsSubclassOf(typeof(Hub<>)));

        public ArgumentInfo MapHubMethods(string name, Type propertyType)
        {
            _typeMapper.MapType(propertyType, name);

            var typeName = propertyType.Name;
            if (propertyType.IsCollection())
            {
                return _typeMapper.MapCollectionType(name, propertyType);
            }

            return new ArgumentInfo
            {
                Name = name.ToCamelCase(),
                Type = typeName
            };
        }
    }
}
