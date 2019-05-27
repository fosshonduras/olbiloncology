using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Queries
{
    public class GetTransfusionVitalSignsDetailsListQuery : GetListBase, IRequest<ListModel<TransfusionVitalSignsDetailModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetTransfusionVitalSignsDetailsListQuery, ListModel<TransfusionVitalSignsDetailModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionVitalSignsDetailModel>> Handle(GetTransfusionVitalSignsDetailsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<TransfusionVitalSignsDetail>( i => i.TransfusionVitalSignsDetailId );

                return await RetrieveListResults<TransfusionVitalSignsDetail, TransfusionVitalSignsDetailModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
