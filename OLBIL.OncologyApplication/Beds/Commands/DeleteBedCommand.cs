using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class DeleteBedCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteBedCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteBedCommand request, CancellationToken cancellationToken)
            {
                var building = await Context.Beds
                    .Where(p => p.BedId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building == null)
                {
                    throw new NotFoundException(nameof(Bed), nameof(building.BedId), request.Id);
                }

                Context.Beds.Remove(building);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
