using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingsListQuery: IRequest<ListModel<BuildingModel>>
    {
        public class Handler : IRequestHandler<GetBuildingsListQuery, ListModel<BuildingModel>>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListModel<BuildingModel>> Handle(GetBuildingsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BuildingModel>
                {
                    Items = await _context.Buildings
                                       .ProjectTo<BuildingModel>(_mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
