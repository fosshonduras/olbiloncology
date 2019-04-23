using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.EvolutionCards.Queries
{
    public class GetEvolutionCardsListQuery : GetListBase, IRequest<ListModel<EvolutionCardModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetEvolutionCardsListQuery, ListModel<EvolutionCardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<EvolutionCardModel>> Handle(GetEvolutionCardsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<EvolutionCardModel>
                {
                    Items = await Context.EvolutionCards
                                       .ProjectTo<EvolutionCardModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
