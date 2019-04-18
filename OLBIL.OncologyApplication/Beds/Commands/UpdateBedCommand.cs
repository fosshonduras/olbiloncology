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
    public class UpdateBedCommand: IRequest
    {
        public BedModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateBedCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateBedCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Beds
                    .Where(p => p.BedId == model.BedId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Bed), nameof(model.BedId), model.BedId);
                }

                item.Name = model.Name;
                item.LongDescription = model.LongDescription;
                item.WardId = model.WardId.Value;
                item.BedStatusId = model.BedStatusId.Value;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
