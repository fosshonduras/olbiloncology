
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.EvolutionCards.Queries
{
    public class GetEvolutionCardQuery : IRequest<EvolutionCardModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetEvolutionCardQuery, EvolutionCardModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<EvolutionCardModel> Handle(GetEvolutionCardQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<EvolutionCardModel>(await Context
                    .EvolutionCards.Where(o => o.EvolutionCardId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(EvolutionCard), nameof(item.EvolutionCardId), request.Id);
                }

                return item;
            }
        }
    }
}