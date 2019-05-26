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

namespace OLBIL.OncologyApplication.EvolutionCards.Queries
{
    public class SearchEvolutionCardsQuery : SearchBase, IRequest<ListModel<EvolutionCardModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchEvolutionCardsQuery, ListModel<EvolutionCardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<EvolutionCardModel>> Handle(SearchEvolutionCardsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<EvolutionCard, bool>> predicate = i => EF.Functions.ILike(i.Observations, $"%{request.SearchTerm}%")
                                     || EF.Functions.ILike(i.ReferredTo, $"%{request.SearchTerm}%")
                                     || EF.Functions.ILike(i.Directions, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<EvolutionCard>(i => i.EvolutionCardId);

                return await RetrieveSearchResults<EvolutionCard, EvolutionCardModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
