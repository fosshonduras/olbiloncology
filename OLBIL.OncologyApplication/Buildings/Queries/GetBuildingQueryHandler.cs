using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingQueryHandler : IRequestHandler<GetBuildingQuery, BuildingModel>
    {     
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetBuildingQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BuildingModel> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<BuildingModel>(await _context
                .Buildings.Where(o => o.BuildingId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if(item == null)
            {
                throw new NotFoundException(nameof(Building), nameof(item.BuildingId), request.Id);
            }

            return item;
        }
    }
}
