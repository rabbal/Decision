using AutoMapper;

namespace Decision.AutoMapperProfiles.Extentions
{
    public static class AutoIgnore
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            foreach (var property in expression.TypeMap.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, a => a.Ignore());
            }
            return expression;
        }
    }
}
