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

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public sealed class SearchWardsQuery : SearchBase, IRequest<ListModel<WardModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchWardsQuery, ListModel<WardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<WardModel>> Handle(SearchWardsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Ward, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<Ward, WardModel>(predicate, request, cancellationToken);
            }
        }
    }
}
