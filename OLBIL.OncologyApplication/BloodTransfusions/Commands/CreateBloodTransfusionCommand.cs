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

namespace OLBIL.OncologyApplication.BloodTransfusions.Commands
{
    public class CreateBloodTransfusionCommand : IRequest<int>
    {
        public BloodTransfusionModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateBloodTransfusionCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateBloodTransfusionCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.BloodTransfusions
                    .Where(p => p.BloodTransfusionId == model.BloodTransfusionId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(BloodTransfusion), nameof(model.BloodTransfusionId), model.BloodTransfusionId);
                }

                var newRecord = new BloodTransfusion
                {
                    OncologyPatientId = model.OncologyPatientId.Value,
                    Group = model.Group,
                    WardId = model.WardId.Value,
                    ABORH = model.ABORH,
                    Date = model.Date.Value,
                    VerifyBy = model.VerifyBy,
                };

                Context.BloodTransfusions.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.BloodTransfusionId;
            }
        }
    }
}