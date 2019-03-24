using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetUnitsListQueryHandler : IRequestHandler<GetUnitsListQuery, UnitsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetUnitsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UnitsListModel> Handle(GetUnitsListQuery request, CancellationToken cancellationToken)
        {
            return new UnitsListModel
            {
                Items = await _context.Units
                                   .ProjectTo<UnitModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}