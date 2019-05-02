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

namespace OLBIL.OncologyApplication.PatientPhysicalRecords.Queries
{
    public class GetPatientPhysicalRecordQuery: IRequest<PatientPhysicalRecordModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetPatientPhysicalRecordQuery, PatientPhysicalRecordModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<PatientPhysicalRecordModel> Handle(GetPatientPhysicalRecordQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<PatientPhysicalRecordModel>(await Context
                    .PatientPhysicalRecords.Where(o => o.PatientPhysicalRecordId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(PatientPhysicalRecord), nameof(item.PatientPhysicalRecordId), request.Id);
                }

                return item;
            }
        }
    }
}
