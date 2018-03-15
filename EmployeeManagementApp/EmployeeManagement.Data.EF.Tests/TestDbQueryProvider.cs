using System.Linq;
using System.Linq.Expressions;

public class TestDbQueryProvider<TEntity> : IQueryProvider
{
    private readonly IQueryProvider _inner;

    internal TestDbQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new System.NotImplementedException();
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        throw new System.NotImplementedException();
    }

    public object Execute(Expression expression)
    {
        throw new System.NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        throw new System.NotImplementedException();
    }
}