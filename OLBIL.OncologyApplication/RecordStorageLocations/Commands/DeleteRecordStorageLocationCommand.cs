using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.RecordStorageLocations.Commands
{
    public class DeleteRecordStorageLocationCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteRecordStorageLocationCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteRecordStorageLocationCommand request, CancellationToken cancellationToken)
            {
                var building = await Context.RecordStorageLocations
                    .Where(p => p.RecordStorageLocationId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (building == null)
                {
                    throw new NotFoundException(nameof(RecordStorageLocation), nameof(building.RecordStorageLocationId), request.Id);
                }

                Context.RecordStorageLocations.Remove(building);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
