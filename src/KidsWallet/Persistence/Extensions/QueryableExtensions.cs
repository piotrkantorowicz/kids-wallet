using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace KidsWallet.Persistence.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Tracking<T>(this IQueryable<T> queryable, bool? useTracking) where T : class
    {
        return useTracking == true ? queryable : queryable.AsNoTracking();
    }

    public static IQueryable<T> IncludeMany<T>(this IQueryable<T> queryable,
        IEnumerable<Expression<Func<T, object>>> includeExpression) where T : class
    {
        return includeExpression.Aggregate(seed: queryable,
            func: (current, include) => current.IncludeIfNotNull(includeExpression: include));
    }

    public static IQueryable<T> IncludeMany<T>(this IQueryable<T> queryable, string[] includes) where T : class
    {
        return includes.Aggregate(seed: queryable,
            func: (current, include) => current.IncludeIfNotNull(include: include));
    }

    private static IQueryable<T> IncludeIfNotNull<T>(this IQueryable<T> queryable,
        Expression<Func<T, object>>? includeExpression) where T : class
    {
        return includeExpression != null ? queryable.Include(navigationPropertyPath: includeExpression) : queryable;
    }

    private static IQueryable<T> IncludeIfNotNull<T>(this IQueryable<T> queryable, string? include) where T : class
    {
        return !string.IsNullOrEmpty(value: include) ? queryable.Include(navigationPropertyPath: include) : queryable;
    }
}