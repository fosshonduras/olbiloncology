using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
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

        public class Handler : HandlerBase, IRequestHandler<CreateBuildingCommand, int>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var building = await Context.Buildings
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

                Context.Buildings.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.BuildingId;
            }
        }
    }
}
