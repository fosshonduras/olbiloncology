using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionPhases.Commands
{
    public class DeleteTransfusionPhaseCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteTransfusionPhaseCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(DeleteTransfusionPhaseCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.TransfusionPhases
                    .Where(p => p.TransfusionPhaseId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionPhase), nameof(item.TransfusionPhaseId), request.Id);
                }

                Context.TransfusionPhases.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}