using AutoMapper;
using System.Reflection;

namespace Poketto.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMappableFrom<>)) && !t.IsAbstract)
                                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("CreateMap")
                    ?? type.GetInterface("IMappableFrom`1")?.GetMethod("CreateMap");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
