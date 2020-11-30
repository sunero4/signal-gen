using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalGen.Models;

namespace SignalGen.Interfaces
{
    public interface ITypeMapper
    {
        List<TypeDescription> CustomTypes { get; }
        TypeDescription MapType(Type type, string propertyName);
        ArgumentInfo MapCollectionType(string name, Type collectionType);
    }
}
