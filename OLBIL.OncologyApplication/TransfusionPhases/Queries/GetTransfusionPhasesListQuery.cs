using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionPhases.Queries
{
    public class GetTransfusionPhasesListQuery : GetListBase, IRequest<ListModel<TransfusionPhaseModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetTransfusionPhasesListQuery, ListModel<TransfusionPhaseModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionPhaseModel>> Handle(GetTransfusionPhasesListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<TransfusionPhase>( i => i.TransfusionPhaseId );

                return await RetrieveListResults<TransfusionPhase, TransfusionPhaseModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
