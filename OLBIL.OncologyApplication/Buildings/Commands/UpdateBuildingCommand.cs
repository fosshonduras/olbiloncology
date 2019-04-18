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
    public class UpdateBuildingCommand : IRequest
    {
        public BuildingModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateBuildingCommand>
        {
            public Handler(OncologyContext context, IMapper mapper): base(context, mapper) { }

            public async Task<Unit> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Buildings
                    .Where(p => p.BuildingId == model.BuildingId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Building), nameof(model.BuildingId), model.BuildingId);
                }

                item.Code = model.Code;
                item.Name = model.Name;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
