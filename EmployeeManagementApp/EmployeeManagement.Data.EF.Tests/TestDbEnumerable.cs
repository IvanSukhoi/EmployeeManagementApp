using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

internal class TestDbEnumerable<T> : EnumerableQuery<T>, IQueryable
{
    public TestDbEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    { }

    public TestDbEnumerable(Expression expression)
        : base(expression)
    { }

    IQueryProvider IQueryable.Provider => new TestDbQueryProvider<T>(this);
}