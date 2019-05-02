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

namespace OLBIL.OncologyApplication.PhysicalRecordTransfers.Commands
{
    public class DeletePhysicalRecordTransferCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeletePhysicalRecordTransferCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeletePhysicalRecordTransferCommand request, CancellationToken cancellationToken)
            {
                var building = await Context.PhysicalRecordTransfers
                    .Where(p => p.PhysicalRecordTransferId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building == null)
                {
                    throw new NotFoundException(nameof(PhysicalRecordTransfer), nameof(building.PhysicalRecordTransferId), request.Id);
                }

                Context.PhysicalRecordTransfers.Remove(building);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
