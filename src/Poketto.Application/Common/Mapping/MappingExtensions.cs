using AutoMapper;

namespace Poketto.Application.Common.Mapping;

public static class MappingExtensions
{
    public static IEnumerable<TDestination> MapToHierarchy<TSource, TDestination>(
        this IEnumerable<TSource> source,
        string sourceChildrenPropName,
        string destinationChildrenPropName,
        IMapper mapper)
    {
        var result = new List<TDestination>();
        if (source == null || !source.Any())
        {
            return result.AsQueryable();
        }
        else
        {
            foreach (var item in source)
            {
                var destination = mapper.Map<TDestination>(item);
                if (destination != null && item != null)
                {
                    var destChildren = destination.GetType().GetProperty(destinationChildrenPropName);
                    var sourceChildren = item.GetType().GetProperty(sourceChildrenPropName);
                    if (destChildren != null && destChildren.CanWrite && sourceChildren != null)
                    {
                        var mappedChildren = MapToHierarchy<TSource, TDestination>(
                            sourceChildren.GetValue(item, null) as IEnumerable<TSource> ?? new List<TSource>(),
                            sourceChildrenPropName,
                            destinationChildrenPropName,
                            mapper);
                        destChildren.SetValue(destination, mappedChildren, null);
                        result.Add(destination);
                    }
                }
            }
        }
        return result;
    }
}