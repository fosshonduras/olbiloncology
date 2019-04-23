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

namespace OLBIL.OncologyApplication.EvolutionCards.Commands
{
    public class UpdateEvolutionCardCommand : IRequest
    {
        public EvolutionCardModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateEvolutionCardCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateEvolutionCardCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.EvolutionCards
                    .Where(p => p.EvolutionCardId == model.EvolutionCardId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(EvolutionCard), nameof(model.EvolutionCardId), model.EvolutionCardId);
                }

                item.OncologyPatientId = model.OncologyPatientId.Value;
                item.AppointmentId = model.AppointmentId;
                item.Directions = model.Directions;
                item.DiagnosisId = model.DiagnosisId;
                item.HeightCm = model.HeightCm;
                item.WeightKg = model.WeightKg;
                item.HeartBeatRateBpm = model.HeartBeatRateBpm;
                item.TemperatureC = model.TemperatureC;
                item.HealthProfessionalId = model.HealthProfessionalId;
                item.Observations = model.Observations;
                item.ReferredTo = model.ReferredTo;
                item.NextAppointmentDate = model.NextAppointmentDate;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}