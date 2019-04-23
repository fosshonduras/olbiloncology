using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class CreateWardCommand : IRequest<int>
    {
        public WardModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateWardCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateWardCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Wards
                    .Where(p => p.WardId == model.WardId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(Ward), nameof(model.WardId), model.WardId);
                }

                var newRecord = new Ward
                {
                    Name = model.Name,
                    BuildingId = model.BuildingId.Value,
                    FloorNumber = model.FloorNumber.Value,
                    HospitalUnitId = model.HospitalUnitId.Value,
                    WardGenderId = model.WardGenderId.Value,
                    WardStatusId = model.WardStatusId.Value
                };

                Context.Wards.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.WardId;
            }
        }
    }
}