using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class DeleteBuildingCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteBuildingCommand>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
            {
                var building = await _context.Buildings
                    .Where(p => p.BuildingId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building == null)
                {
                    throw new NotFoundException(nameof(Building), nameof(building.BuildingId), request.Id);
                }

                _context.Buildings.Remove(building);

                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
