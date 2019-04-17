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
    public class GetHospitalUnitsListQueryHandler : IRequestHandler<GetHospitalUnitsListQuery, ListModel<HospitalUnitModel>>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetHospitalUnitsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListModel<HospitalUnitModel>> Handle(GetHospitalUnitsListQuery request, CancellationToken cancellationToken)
        {
            return new ListModel<HospitalUnitModel>
            {
                Items = await _context.HospitalUnits
                                   .ProjectTo<HospitalUnitModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}