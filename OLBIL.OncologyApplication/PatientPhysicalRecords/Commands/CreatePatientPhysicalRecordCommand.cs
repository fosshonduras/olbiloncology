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
    public class CreatePatientPhysicalRecordCommand: IRequest<int>
    {
        public PatientPhysicalRecordModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreatePatientPhysicalRecordCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreatePatientPhysicalRecordCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.PatientPhysicalRecords
                    .Where(p => p.PatientPhysicalRecordId == model.PatientPhysicalRecordId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(PatientPhysicalRecord), nameof(model.PatientPhysicalRecordId), model.PatientPhysicalRecordId);
                }

                var newRecord = new PatientPhysicalRecord
                {
                    RecordNumber = model.RecordNumber,
                    RecordStorageLocationId = model.RecordStorageLocationId.Value,
                    OncologyPatientId = model.OncologyPatientId.Value
                };

                Context.PatientPhysicalRecords.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.PatientPhysicalRecordId;
            }
        }
    }
}
