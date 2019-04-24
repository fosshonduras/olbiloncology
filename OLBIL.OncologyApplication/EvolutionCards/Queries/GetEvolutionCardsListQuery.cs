using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.EvolutionCards.Queries
{
    public class GetEvolutionCardsListQuery : GetListBase, IRequest<ListModel<EvolutionCardModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetEvolutionCardsListQuery, ListModel<EvolutionCardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<EvolutionCardModel>> Handle(GetEvolutionCardsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<EvolutionCard, EvolutionCardModel>(null, request, cancellationToken);
            }
        }
    }
}
