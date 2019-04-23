using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class GetListHandlerBase: HandlerBase
    {
        public GetListHandlerBase(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

        protected IQueryable<T> ApplyPaging<T>(GetListBase request, IQueryable<T> filteredQuery) where T : class
        {
            return filteredQuery
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize);
        }

        protected async Task<ListModel<TResult>> ProjectTo<TSource, TResult>(IQueryable<TSource> pagedQuery, GetListBase request, int totalCount, CancellationToken cancellationToken)
            where TSource : class
            where TResult : class
        {
            return new ListModel<TResult>
            {
                TotalCount = totalCount,
                Items = await pagedQuery.ProjectTo<TResult>(Mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken)
            };
        }

        protected async Task<ListModel<TResult>> RetrieveListResults<TSource, TResult>(Expression<Func<TSource, bool>> predicate, GetListBase request, CancellationToken cancellationToken)
            where TSource : class
            where TResult : class
        {
            var filteredQuery = Context.Set<TSource>().AsQueryable();

            if(predicate != null)
            {
                filteredQuery = filteredQuery.Where(predicate);
            }
            var count = await filteredQuery.CountAsync();
            var pagedQuery = ApplyPaging(request, filteredQuery);
            return await ProjectTo<TSource, TResult>(pagedQuery, request, count, cancellationToken);
        }
    }
}
