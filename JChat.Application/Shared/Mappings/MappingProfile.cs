using System.Reflection;
using AutoMapper;
using JChat.Application.Shared.Dtos;
using JChat.Domain.Entities.Message;

namespace JChat.Application.Shared.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsForAssembly(Assembly.GetExecutingAssembly());
        ApplyManualMappings();
    }

    private void ApplyMappingsForAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }

    private void ApplyManualMappings()
    {
        CreateMap<Reaction, IdNameDto>();
        CreateMap<MessagePriority, IdNameDto>();
        CreateMap<MessageBodyType, IdNameDto>();
    }
}
