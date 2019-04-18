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

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class CreateBedCommand: IRequest<int>
    {
        public BedModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateBedCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateBedCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Beds
                    .Where(p => p.BedId == model.BedId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(Bed), nameof(model.BedId), model.BedId);
                }

                var newRecord = new Bed
                {
                    Name = model.Name,
                    LongDescription = model.LongDescription,
                    WardId = model.WardId.Value,
                    BedStatusId = model.BedStatusId.Value
                };

                Context.Beds.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.BedId;
            }
        }
    }
}
