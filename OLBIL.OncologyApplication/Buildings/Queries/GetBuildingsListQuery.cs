using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingsListQuery: IRequest<ListModel<BuildingModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetBuildingsListQuery, ListModel<BuildingModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BuildingModel>> Handle(GetBuildingsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BuildingModel>
                {
                    Items = await Context.Buildings
                                       .ProjectTo<BuildingModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
