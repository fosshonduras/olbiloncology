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
    public class GetHospitalUnitsListQueryHandler : IRequestHandler<GetHospitalUnitsListQuery, HospitalUnitsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetHospitalUnitsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HospitalUnitsListModel> Handle(GetHospitalUnitsListQuery request, CancellationToken cancellationToken)
        {
            return new HospitalUnitsListModel
            {
                Items = await _context.Units
                                   .ProjectTo<HospitalUnitModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}