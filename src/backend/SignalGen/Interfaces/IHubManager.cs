using System;
using System.Collections.Generic;
using SignalGen.Models;

namespace SignalGen.Interfaces
{
    public interface IHubManager
    {
        IEnumerable<Type> GetHubClasses();
        ArgumentInfo MapHubMethods(string name, Type propertyType);
    }
}