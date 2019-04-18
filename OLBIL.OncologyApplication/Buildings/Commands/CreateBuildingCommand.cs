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

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class CreateBuildingCommand: IRequest<int>
    {
        public BuildingModel Model { get; set; }

        public class Handler : IRequestHandler<CreateBuildingCommand, int>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var building = await _context.Buildings
                    .Where(p => p.Code == model.Code)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building != null)
                {
                    throw new AlreadyExistsException(nameof(Building), nameof(model.Code), model.Code);
                }

                var newRecord = new Building
                {
                    Code = model.Code,
                    Name = model.Name
                };

                _context.Buildings.Add(newRecord);
                await _context.SaveChangesAsync(cancellationToken);

                return newRecord.BuildingId;
            }
        }
    }
}
