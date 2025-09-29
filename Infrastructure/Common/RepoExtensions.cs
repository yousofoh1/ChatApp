using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common;

public static class RepoExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> values, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return values;

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<T>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return values;

        return values.OrderBy(orderQuery);
    }

    public static IQueryable<T> Search<T>(this IQueryable<T> query, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return query;

        var properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => Attribute.IsDefined(p, typeof(SearchableAttribute)) &&
                        (p.PropertyType == typeof(string))) // optional: limit to strings
            .ToList();

        if (!properties.Any())
            return query;

        var conditions = properties
            .Select(p => $"{p.Name}.Contains(@0)") // builds: Title.Contains(@0) OR City.Contains(@0)
            .ToList();

        var fullCondition = string.Join(" OR ", conditions);

        return query.Where(fullCondition, searchTerm);
    }
}
