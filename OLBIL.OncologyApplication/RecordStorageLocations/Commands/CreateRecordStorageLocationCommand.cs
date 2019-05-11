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
    public class CreateRecordStorageLocationCommand: IRequest<int>
    {
        public RecordStorageLocationModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateRecordStorageLocationCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateRecordStorageLocationCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.RecordStorageLocations
                    .Where(p => p.RecordStorageLocationId == model.RecordStorageLocationId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(RecordStorageLocation), nameof(model.RecordStorageLocationId), model.RecordStorageLocationId);
                }

                var newRecord = new RecordStorageLocation
                {
                    Name = model.Name,
                    ParentLocationId = model.ParentLocationId
                };

                Context.RecordStorageLocations.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.RecordStorageLocationId;
            }
        }
    }
}
