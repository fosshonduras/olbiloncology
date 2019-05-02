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

namespace OLBIL.OncologyApplication.PhysicalRecordTransfers.Commands
{
    public class UpdatePhysicalRecordTransferCommand: IRequest
    {
        public PhysicalRecordTransferModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdatePhysicalRecordTransferCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdatePhysicalRecordTransferCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.PhysicalRecordTransfers
                    .Where(p => p.PhysicalRecordTransferId == model.PhysicalRecordTransferId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(PhysicalRecordTransfer), nameof(model.PhysicalRecordTransferId), model.PhysicalRecordTransferId);
                }

                item.PatientPhysicalRecordId = model.PatientPhysicalRecordId.Value;
                item.TargetLocationId = model.TargetLocationId.Value;
                item.DeliveredBy = model.DeliveredBy;
                item.ReceivedBy = model.ReceivedBy;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
