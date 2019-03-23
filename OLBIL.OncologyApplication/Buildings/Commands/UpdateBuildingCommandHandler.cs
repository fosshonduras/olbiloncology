using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class UpdateBuildingCommandHandler : IRequestHandler<UpdateBuildingCommand>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public UpdateBuildingCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var building = await _context.Buildings
                .Where(p => p.BuildingId == model.BuildingId)
                .FirstOrDefaultAsync(cancellationToken);
            if (building == null)
            {
                throw new NotFoundException(nameof(Building), nameof(model.BuildingId), model.BuildingId);
            }

            building.Code = model.Code;
            building.Name = model.Name;

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
