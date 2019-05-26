using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.PhysicalRecordTransfers.Queries
{
    public class GetPhysicalRecordTransfersListQuery: GetListBase, IRequest<ListModel<PhysicalRecordTransferModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetPhysicalRecordTransfersListQuery, ListModel<PhysicalRecordTransferModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<PhysicalRecordTransferModel>> Handle(GetPhysicalRecordTransfersListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<PhysicalRecordTransfer>(i => i.PhysicalRecordTransferId);

                return await RetrieveListResults<PhysicalRecordTransfer, PhysicalRecordTransferModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
