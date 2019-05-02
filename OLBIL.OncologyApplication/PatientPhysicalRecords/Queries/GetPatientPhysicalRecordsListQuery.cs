using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.PatientPhysicalRecords.Queries
{
    public class GetPatientPhysicalRecordsListQuery: GetListBase, IRequest<ListModel<PatientPhysicalRecordModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetPatientPhysicalRecordsListQuery, ListModel<PatientPhysicalRecordModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<PatientPhysicalRecordModel>> Handle(GetPatientPhysicalRecordsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<PatientPhysicalRecord, PatientPhysicalRecordModel>(null, request, cancellationToken);
            }
        }
    }
}
