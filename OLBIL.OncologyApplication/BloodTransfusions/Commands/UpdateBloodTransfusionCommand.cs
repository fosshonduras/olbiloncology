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

namespace OLBIL.OncologyApplication.BloodTransfusions.Commands
{
    public class UpdateBloodTransfusionCommand : IRequest
    {
        public BloodTransfusionModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateBloodTransfusionCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateBloodTransfusionCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.BloodTransfusions
                    .Where(p => p.BloodTransfusionId == model.BloodTransfusionId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(BloodTransfusion), nameof(model.BloodTransfusionId), model.BloodTransfusionId);
                }

                item.OncologyPatientId = model.OncologyPatientId.Value;
                item.Group = model.Group;
                item.WardId = model.WardId.Value;
                item.ABORH = model.ABORH;
                item.Date = model.Date.Value;
                item.VerifyBy = model.VerifyBy;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}