using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionPhases.Commands
{
    public class UpdateTransfusionPhaseCommand : IRequest
    {
        public TransfusionPhaseModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateTransfusionPhaseCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateTransfusionPhaseCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionPhases
                    .Where(p => p.TransfusionPhaseId == model.TransfusionPhaseId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionPhase), nameof(model.TransfusionPhaseId), model.TransfusionPhaseId);
                }

                item.Name = model.Name;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}