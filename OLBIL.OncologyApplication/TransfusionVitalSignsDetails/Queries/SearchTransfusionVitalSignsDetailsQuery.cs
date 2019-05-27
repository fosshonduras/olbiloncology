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

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Queries
{
    public sealed class SearchTransfusionVitalSignsDetailsQuery : SearchBase, IRequest<ListModel<TransfusionVitalSignsDetailModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchTransfusionVitalSignsDetailsQuery, ListModel<TransfusionVitalSignsDetailModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionVitalSignsDetailModel>> Handle(SearchTransfusionVitalSignsDetailsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<TransfusionVitalSignsDetail, bool>> predicate = i => EF.Functions.ILike(i.Observations, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<TransfusionVitalSignsDetail>(i => i.TransfusionVitalSignsDetailId);

                return await RetrieveSearchResults<TransfusionVitalSignsDetail, TransfusionVitalSignsDetailModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
