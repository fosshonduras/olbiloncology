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

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Commands
{
    public class CreateTransfusionVitalSignsDetailCommand : IRequest<int>
    {
        public TransfusionVitalSignsDetailModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateTransfusionVitalSignsDetailCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateTransfusionVitalSignsDetailCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionVitalSignsDetails
                    .Where(p => p.TransfusionVitalSignsDetailId == model.TransfusionVitalSignsDetailId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(TransfusionVitalSignsDetail), nameof(model.TransfusionVitalSignsDetailId), model.TransfusionVitalSignsDetailId);
                }

                var newRecord = new TransfusionVitalSignsDetail
                {
                    BloodTransfusionId = model.BloodTransfusionId.Value,
                    TransfusionPhaseId = model.TransfusionPhaseId.Value,
                    ArterialPressure = model.ArterialPressure.Value,
                    TemperatureC = model.TemperatureC.Value,
                    RespiratoryFrequence = model.RespiratoryFrequence.Value,
                    HeartbeatRateBpm = model.HeartbeatRateBpm.Value,
                    Responsible = model.Responsible,
                    Observations = model.Observations,
                };

                Context.TransfusionVitalSignsDetails.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.TransfusionVitalSignsDetailId;
            }
        }
    }
}