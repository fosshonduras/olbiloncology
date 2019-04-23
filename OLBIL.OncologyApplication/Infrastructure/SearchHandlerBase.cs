using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class SearchHandlerBase: GetListHandlerBase
    {
        public SearchHandlerBase(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

        protected IQueryable<T> ApplyFilters<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Context.Set<T>().Where(predicate);
        }

        protected async Task<ListModel<TResult>> RetrieveSearchResults<TSource, TResult>(Expression<Func<TSource, bool>> predicate, SearchBase request, CancellationToken cancellationToken)
            where TSource : class
            where TResult : class
        {
            var filteredQuery = ApplyFilters(predicate);
            var count = await filteredQuery.CountAsync();
            var pagedQuery = ApplyPaging(request, filteredQuery);
            return await ProjectTo<TSource, TResult>(pagedQuery,  request, count, cancellationToken);
        }
    }
}
