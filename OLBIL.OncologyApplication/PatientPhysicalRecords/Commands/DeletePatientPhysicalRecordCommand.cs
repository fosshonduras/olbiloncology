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

namespace OLBIL.OncologyApplication.PatientPhysicalRecords.Commands
{
    public class DeletePatientPhysicalRecordCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeletePatientPhysicalRecordCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeletePatientPhysicalRecordCommand request, CancellationToken cancellationToken)
            {
                var building = await Context.PatientPhysicalRecords
                    .Where(p => p.PatientPhysicalRecordId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building == null)
                {
                    throw new NotFoundException(nameof(PatientPhysicalRecord), nameof(building.PatientPhysicalRecordId), request.Id);
                }

                Context.PatientPhysicalRecords.Remove(building);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
