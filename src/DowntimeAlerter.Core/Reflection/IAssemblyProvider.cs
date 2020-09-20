using System.Collections.Generic;
using System.Reflection;

namespace DowntimeAlerter.Reflection
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();

        IList<string> AdditionalAssemblyNames { get; set; }
    }
}