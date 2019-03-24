using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public DeleteUnitCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Units
                .Where(p => p.UnitId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (item == null)
            {
                throw new NotFoundException(nameof(HospitalUnit), nameof(item.UnitId), request.Id);
            }

            _context.Units.Remove(item);

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}