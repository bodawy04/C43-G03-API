using System.Linq.Expressions;

namespace Domain.Contracts;

public interface ISpecifications<T> where T : class
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> IncludeExpressions { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    public int Skip { get; }
    public int Take { get; }
    public bool IsPaginated { get; }
}

// Where => Criteria Expression<Func<T,bool>>
// Include => List<Expression<Func<T, object>>>
// Select => Criteria Expression<Func<T, TResult>>