using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.RecordStorageLocations.Commands
{
    public class UpdateRecordStorageLocationCommand: IRequest
    {
        public RecordStorageLocationModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateRecordStorageLocationCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateRecordStorageLocationCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.RecordStorageLocations
                    .Where(p => p.RecordStorageLocationId == model.RecordStorageLocationId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(RecordStorageLocation), nameof(model.RecordStorageLocationId), model.RecordStorageLocationId);
                }

                item.Name = model.Name;

                item.ParentLocationId = model.ParentLocationId.Value;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
