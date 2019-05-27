using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Queries
{
    public class GetTransfusionProductDetailsListQuery : GetListBase, IRequest<ListModel<TransfusionProductDetailModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetTransfusionProductDetailsListQuery, ListModel<TransfusionProductDetailModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionProductDetailModel>> Handle(GetTransfusionProductDetailsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<TransfusionProductDetail>( i => i.TransfusionProductDetailId );

                return await RetrieveListResults<TransfusionProductDetail, TransfusionProductDetailModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
