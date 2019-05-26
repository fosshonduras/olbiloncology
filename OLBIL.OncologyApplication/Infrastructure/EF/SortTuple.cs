using System;
using System.Linq.Expressions;

namespace OLBIL.OncologyApplication.Infrastructure.EF
{
    public class SortTuple<TSource> where TSource : class
    {
        public Expression<Func<TSource, object>> Expression { get; set; }
        public bool IsDescending { get; set; } = false;
    }
}
