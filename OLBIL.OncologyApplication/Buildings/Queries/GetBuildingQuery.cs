using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingQuery : IRequest<BuildingModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetBuildingQuery, BuildingModel>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<BuildingModel> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<BuildingModel>(await Context
                    .Buildings.Where(o => o.BuildingId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Building), nameof(item.BuildingId), request.Id);
                }

                return item;
            }
        }
    }
}
