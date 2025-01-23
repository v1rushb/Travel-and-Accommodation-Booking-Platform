using System.Linq.Expressions;

namespace TABP.Application.Extensions;

public static class Expressions
{
    public static Expression<Func<T, bool>> True<T>() => _ => true;

    public static Expression<Func<T, bool>> False<T>() => _ => false; // hmm consider deleting if not needed in the futrue.

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(expr1.Body, invokedExpr),
            expr1.Parameters
        );
    }

    public static Expression<Func<T, bool>> AndIf<T>( // consider doing Lazy evaluation or quit.
        this Expression<Func<T, bool>> expr,
        bool condition,
        Expression<Func<T, bool>> newExpr)
    {
        return condition ? expr.And(newExpr) : expr;
    }
}