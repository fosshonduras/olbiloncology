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

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class UpdateWardCommand: IRequest
    {
        public WardModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateWardCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Wards
                    .Where(p => p.WardId == model.WardId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Ward), nameof(model.WardId), model.WardId);
                }

                item.BuildingId = model.BuildingId.Value;
                item.FloorNumber = model.FloorNumber.Value;
                item.Name = model.Name;
                item.HospitalUnitId = model.HospitalUnitId.Value;
                item.WardGenderId = model.WardGenderId.Value;
                item.WardStatusId = model.WardStatusId.Value;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
