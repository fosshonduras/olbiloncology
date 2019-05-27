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

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Commands
{
    public class UpdateTransfusionVitalSignsDetailCommand : IRequest
    {
        public TransfusionVitalSignsDetailModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateTransfusionVitalSignsDetailCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateTransfusionVitalSignsDetailCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionVitalSignsDetails
                    .Where(p => p.TransfusionVitalSignsDetailId == model.TransfusionVitalSignsDetailId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionVitalSignsDetail), nameof(model.TransfusionVitalSignsDetailId), model.TransfusionVitalSignsDetailId);
                }

                item.BloodTransfusionId = model.BloodTransfusionId.Value;
                item.TransfusionPhaseId = model.TransfusionPhaseId.Value;
                item.ArterialPressure = model.ArterialPressure.Value;
                item.TemperatureC = model.TemperatureC.Value;
                item.RespiratoryFrequence = model.RespiratoryFrequence.Value;
                item.HeartbeatRateBpm = model.HeartbeatRateBpm.Value;
                item.Responsible = model.Responsible;
                item.Observations = model.Observations;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}