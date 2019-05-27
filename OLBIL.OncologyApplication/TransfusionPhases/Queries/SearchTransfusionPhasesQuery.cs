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

namespace OLBIL.OncologyApplication.TransfusionPhases.Queries
{
    public sealed class SearchTransfusionPhasesQuery : SearchBase, IRequest<ListModel<TransfusionPhaseModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchTransfusionPhasesQuery, ListModel<TransfusionPhaseModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionPhaseModel>> Handle(SearchTransfusionPhasesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<TransfusionPhase, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<TransfusionPhase>(i => i.TransfusionPhaseId);

                return await RetrieveSearchResults<TransfusionPhase, TransfusionPhaseModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
