
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

namespace OLBIL.OncologyApplication.TransfusionPhases.Queries
{
    public class GetTransfusionPhaseQuery : IRequest<TransfusionPhaseModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetTransfusionPhaseQuery, TransfusionPhaseModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<TransfusionPhaseModel> Handle(GetTransfusionPhaseQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<TransfusionPhaseModel>(await Context
                    .TransfusionPhases.Where(o => o.TransfusionPhaseId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionPhase), nameof(item.TransfusionPhaseId), request.Id);
                }

                return item;
            }
        }
    }
}