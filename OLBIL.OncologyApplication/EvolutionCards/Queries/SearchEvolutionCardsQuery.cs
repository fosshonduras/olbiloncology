using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.EvolutionCards.Queries
{
    public class SearchEvolutionCardsQuery : SearchBase, IRequest<ListModel<EvolutionCardModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<SearchEvolutionCardsQuery, ListModel<EvolutionCardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<EvolutionCardModel>> Handle(SearchEvolutionCardsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<EvolutionCardModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<EvolutionCardModel>> ApplyFilter(SearchEvolutionCardsQuery request, CancellationToken cancellationToken)
            {
                return await Context.EvolutionCards
                                    .Where(i =>
                                        EF.Functions.ILike(i.Observations, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.ReferredTo, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Directions, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<EvolutionCardModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
