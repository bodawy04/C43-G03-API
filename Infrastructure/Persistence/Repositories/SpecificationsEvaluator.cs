﻿namespace Persistence.Repositories;

internal static class SpecificationsEvaluator
{
    public static IQueryable<T> CreateQuery<T>(
        IQueryable<T> inputQuery,
        ISpecifications<T> specifications)
        where T : class
    {
        var query = inputQuery;
        if (specifications.Criteria is not null)
            query = query.Where(specifications.Criteria);

        //foreach (var includeExpression in specifications.IncludeExpressions)
        //    query = query.Include(includeExpression);
        
        ///UP OR DOWN
        
         query = specifications
            .IncludeExpressions
            .Aggregate(query, (current, include) => current.Include(include));
        
        if(specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);

        if(specifications.IsPaginated)
            query = query.Skip(specifications.Skip).Take(specifications.Take);

        return query;
    }
}
