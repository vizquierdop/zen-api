using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Mappings
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
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                     || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var interfaces = type.GetInterfaces();

                foreach (var i in interfaces)
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    {
                        var method = i.GetMethod("Mapping");
                        method?.Invoke(instance, new object[] { this });
                    }

                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    {
                        var method = i.GetMethod("Mapping");
                        method?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
