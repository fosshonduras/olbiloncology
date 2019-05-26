using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure.EF;
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
    public class GetListHandlerBase : HandlerBase
    {
        public GetListHandlerBase(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

        protected IQueryable<T> ApplyPaging<T>(GetListBase request, IQueryable<T> filteredQuery) where T : class
        {
            return filteredQuery
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize);
        }

        protected async Task<ListModel<TResult>> ProjectTo<TSource, TResult>(IQueryable<TSource> pagedQuery, int totalCount, CancellationToken cancellationToken)
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

        protected async Task<ListModel<TResult>> RetrieveListResults<TSource, TResult>(Expression<Func<TSource, bool>> predicate,
            List<SortTuple<TSource>> orderFunctions,
            GetListBase request, CancellationToken cancellationToken)
            where TSource : class
            where TResult : class
        {
            var filteredQuery = Context.Set<TSource>().AsQueryable();

            if (predicate != null)
            {
                filteredQuery = filteredQuery.Where(predicate);
            }
            var count = filteredQuery.CountAsync();

            var sortedQuery = filteredQuery;
            if (orderFunctions != null)
            {
                sortedQuery = ApplyOrdering(filteredQuery, orderFunctions);
            }

            var pagedQuery = ApplyPaging(request, sortedQuery);
            return await ProjectTo<TSource, TResult>(pagedQuery, await count, cancellationToken);
        }

        protected IQueryable<TSource> ApplyOrdering<TSource>(IQueryable<TSource> filteredQuery, List<SortTuple<TSource>> orderFunctions) where TSource : class
        {
            if (!orderFunctions.Any()) throw new ArgumentException("At least one order function must be supplied");

            IOrderedQueryable<TSource> resultQuery = null;
            bool isFirst = true;
            foreach (var item in orderFunctions)
            {
                if (isFirst)
                {
                    resultQuery = item.IsDescending
                        ? filteredQuery.OrderByDescending(item.Expression)
                        : filteredQuery.OrderBy(item.Expression);
                }
                else
                {
                    resultQuery = item.IsDescending
                        ? resultQuery.ThenByDescending(item.Expression)
                        : resultQuery.ThenBy(item.Expression);
                }
                isFirst = false;
            }

            return resultQuery;
        }

        protected List<SortTuple<TSource>> BuildSortList<TSource>(params Expression<Func<TSource, object>>[] functions) where TSource : class
        {
            return functions.Select(f => new SortTuple<TSource>
            {
                Expression = f
            }).ToList();
        }

        protected List<SortTuple<TSource>> BuildSortList<TSource>(params (Expression<Func<TSource, object>>, bool)[] functions) where TSource : class
        {
            return functions.Select(f => new SortTuple<TSource>
            {
                Expression = f.Item1,
                IsDescending = f.Item2
            }).ToList();
        }
    }
}
