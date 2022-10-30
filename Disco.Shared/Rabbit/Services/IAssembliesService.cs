using System.Reflection;

namespace Disco.Shared.Rabbit.Services;

public interface IAssembliesService
{
    IEnumerable<Type> ReturnTypes();
    IEnumerable<Assembly> ReturnAssemblies();
}