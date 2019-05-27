using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.BloodTransfusions.Queries
{
    public class GetBloodTransfusionsListQuery : GetListBase, IRequest<ListModel<BloodTransfusionModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetBloodTransfusionsListQuery, ListModel<BloodTransfusionModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BloodTransfusionModel>> Handle(GetBloodTransfusionsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<BloodTransfusion>( i => i.BloodTransfusionId );

                return await RetrieveListResults<BloodTransfusion, BloodTransfusionModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
