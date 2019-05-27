using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.TransfusionPhases.Commands
{
    public class CreateTransfusionPhaseCommand : IRequest<int>
    {
        public TransfusionPhaseModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateTransfusionPhaseCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateTransfusionPhaseCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionPhases
                    .Where(p => p.TransfusionPhaseId == model.TransfusionPhaseId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(TransfusionPhase), nameof(model.TransfusionPhaseId), model.TransfusionPhaseId);
                }

                var newRecord = new TransfusionPhase
                {
                    Name = model.Name
                };

                Context.TransfusionPhases.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.TransfusionPhaseId;
            }
        }
    }
}