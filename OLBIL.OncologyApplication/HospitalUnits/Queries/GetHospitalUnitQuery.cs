using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitQuery: IRequest<HospitalUnitModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetHospitalUnitQuery, HospitalUnitModel>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<HospitalUnitModel> Handle(GetHospitalUnitQuery request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<HospitalUnitModel>(await _context
                    .HospitalUnits.Where(o => o.HospitalUnitId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(item.HospitalUnitId), request.Id);
                }

                return item;
            }
        }
    }
}
