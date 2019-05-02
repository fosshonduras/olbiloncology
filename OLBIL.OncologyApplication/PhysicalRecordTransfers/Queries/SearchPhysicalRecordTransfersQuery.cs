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

namespace OLBIL.OncologyApplication.PhysicalRecordTransfers.Queries
{
    public class SearchPhysicalRecordTransfersQuery : SearchBase, IRequest<ListModel<PhysicalRecordTransferModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchPhysicalRecordTransfersQuery, ListModel<PhysicalRecordTransferModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<ListModel<PhysicalRecordTransferModel>> Handle(SearchPhysicalRecordTransfersQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<PhysicalRecordTransfer, bool>> predicate = i => EF.Functions.ILike(i.TargetLocation.Name, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.PatientPhysicalRecord.RecordNumber, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<PhysicalRecordTransfer, PhysicalRecordTransferModel>(predicate, request, cancellationToken);
            }
        }
    }
}
