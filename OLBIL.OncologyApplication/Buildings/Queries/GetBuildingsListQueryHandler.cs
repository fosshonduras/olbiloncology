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
    public class GetBuildingsListQueryHandler : IRequestHandler<GetBuildingsListQuery, BuildingsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetBuildingsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BuildingsListModel> Handle(GetBuildingsListQuery request, CancellationToken cancellationToken)
        {
            return new BuildingsListModel
            {
                Items = await _context.Buildings
                                   .ProjectTo<BuildingModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
