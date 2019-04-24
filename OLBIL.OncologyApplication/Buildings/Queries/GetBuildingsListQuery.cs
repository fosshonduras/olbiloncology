using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingsListQuery: GetListBase, IRequest<ListModel<BuildingModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetBuildingsListQuery, ListModel<BuildingModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BuildingModel>> Handle(GetBuildingsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<Building, BuildingModel>(null, request, cancellationToken);
            }
        }
    }
}
