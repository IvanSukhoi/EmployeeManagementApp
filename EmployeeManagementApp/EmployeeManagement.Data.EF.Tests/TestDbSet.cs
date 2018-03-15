using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeManagement.Data.EF.Tests
{
    public class TestDbSet<T> : IDbSet<T> where T : class, new()
    {
        private readonly List<T> _data = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public Expression Expression => Expression.Constant(_data.AsQueryable());

        public Type ElementType => typeof(T);

        public IQueryProvider Provider => new TestDbQueryProvider<T>(_data.AsQueryable().Provider);

        public T Add(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            _data.Remove(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local => throw new NotImplementedException();
    }
}
