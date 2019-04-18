using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class DeleteHospitalUnitCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteHospitalUnitCommand>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.HospitalUnits
                    .Where(p => p.HospitalUnitId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(item.HospitalUnitId), request.Id);
                }

                _context.HospitalUnits.Remove(item);

                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
