using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardsListQuery : GetListBase, IRequest<ListModel<WardModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetWardsListQuery, ListModel<WardModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<WardModel>> Handle(GetWardsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<Ward, WardModel>(null, request, cancellationToken);
            }
        }
    }
}
