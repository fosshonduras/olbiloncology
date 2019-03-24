using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class DeleteBedCommandHandler: IRequestHandler<DeleteBedCommand>
    {
                private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public DeleteBedCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBedCommand request, CancellationToken cancellationToken)
        {
            var building = await _context.Beds
                .Where(p => p.BedId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (building == null)
            {
                throw new NotFoundException(nameof(Bed), nameof(building.BedId), request.Id);
            }

            _context.Beds.Remove(building);

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
