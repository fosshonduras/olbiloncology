using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class SearchBedsQuery : SearchBase, IRequest<ListModel<BedModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchBedsQuery, ListModel<BedModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<ListModel<BedModel>> Handle(SearchBedsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Bed, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.LongDescription, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<Bed>(i => i.BedId);

                return await RetrieveSearchResults<Bed, BedModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
