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

namespace OLBIL.OncologyApplication.PatientPhysicalRecords.Commands
{
    public class UpdatePatientPhysicalRecordCommand: IRequest
    {
        public PatientPhysicalRecordModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdatePatientPhysicalRecordCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdatePatientPhysicalRecordCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.PatientPhysicalRecords
                    .Where(p => p.PatientPhysicalRecordId == model.PatientPhysicalRecordId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(PatientPhysicalRecord), nameof(model.PatientPhysicalRecordId), model.PatientPhysicalRecordId);
                }

                item.RecordNumber = model.RecordNumber;
                item.OncologyPatientId = model.OncologyPatientId.Value;
                item.RecordStorageLocationId = model.RecordStorageLocationId.Value;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
