using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.Common;
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
    public class CreatePhysicalRecordTransferCommand: IRequest<int>
    {
        public PhysicalRecordTransferModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreatePhysicalRecordTransferCommand, int>
        {
            private readonly IDateTimeProvider _dateTimeProvider;

            public Handler(IOncologyContext context, IMapper mapper, IDateTimeProvider dateTimeProvider) : base(context, mapper) {
                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<int> Handle(CreatePhysicalRecordTransferCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.PhysicalRecordTransfers
                    .Where(p => p.PhysicalRecordTransferId == model.PhysicalRecordTransferId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(PhysicalRecordTransfer), nameof(model.PhysicalRecordTransferId), model.PhysicalRecordTransferId);
                }

                var newRecord = new PhysicalRecordTransfer
                {
                    PatientPhysicalRecordId = model.PatientPhysicalRecordId.Value,
                    TargetLocationId = model.TargetLocationId.Value,
                    DeliveredBy = model.DeliveredBy,
                    ReceivedBy = model.ReceivedBy,
                    Date = _dateTimeProvider.Now
                };

                Context.PhysicalRecordTransfers.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.PhysicalRecordTransferId;
            }
        }
    }
}
