using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class SearchBuildingsListQueryHandler : IRequestHandler<SearchBuildingsQuery, ListModel<BuildingModel>>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchBuildingsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListModel<BuildingModel>> Handle(SearchBuildingsQuery request, CancellationToken cancellationToken)
        {
            return new ListModel<BuildingModel>
            {
                Items = await _context.Buildings
                                   .Where(i =>
                                        EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%")
                                    )
                                   .ProjectTo<BuildingModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
