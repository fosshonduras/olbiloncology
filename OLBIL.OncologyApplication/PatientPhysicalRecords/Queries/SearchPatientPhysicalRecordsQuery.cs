using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.PatientPhysicalRecords.Queries
{
    public class SearchPatientPhysicalRecordsQuery : SearchBase, IRequest<ListModel<PatientPhysicalRecordModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchPatientPhysicalRecordsQuery, ListModel<PatientPhysicalRecordModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<ListModel<PatientPhysicalRecordModel>> Handle(SearchPatientPhysicalRecordsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<PatientPhysicalRecord, bool>> predicate = i => EF.Functions.ILike(i.RecordNumber, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.RecordStorageLocation.Name, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<PatientPhysicalRecord, PatientPhysicalRecordModel>(predicate, request, cancellationToken);
            }
        }
    }
}
