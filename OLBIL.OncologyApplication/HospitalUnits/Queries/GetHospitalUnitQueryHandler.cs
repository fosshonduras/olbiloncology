using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitQueryHandler : IRequestHandler<GetHospitalUnitQuery, HospitalUnitModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetHospitalUnitQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HospitalUnitModel> Handle(GetHospitalUnitQuery request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<HospitalUnitModel>(await _context
                .Units.Where(o => o.HospitalUnitId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if(item == null)
            {
                throw new NotFoundException(nameof(HospitalUnit), nameof(item.HospitalUnitId), request.Id);
            }

            return item;
        }
    }
}
