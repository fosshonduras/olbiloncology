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
    public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, UnitModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetUnitQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UnitModel> Handle(GetUnitQuery request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<UnitModel>(await _context
                .Units.Where(o => o.UnitId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if(item == null)
            {
                throw new NotFoundException(nameof(HospitalUnit), nameof(item.UnitId), request.Id);
            }

            return item;
        }
    }
}
