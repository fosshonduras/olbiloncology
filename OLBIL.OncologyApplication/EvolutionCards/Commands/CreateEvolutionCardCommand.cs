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

namespace OLBIL.OncologyApplication.EvolutionCards.Commands
{
    public class CreateEvolutionCardCommand : IRequest<int>
    {
        public EvolutionCardModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateEvolutionCardCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateEvolutionCardCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.EvolutionCards
                    .Where(p => p.EvolutionCardId == model.EvolutionCardId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(EvolutionCard), nameof(model.EvolutionCardId), model.EvolutionCardId);
                }

                var newRecord = new EvolutionCard
                {
                    OncologyPatientId = model.OncologyPatientId.Value,
                    AppointmentId = model.AppointmentId,
                    Directions = model.Directions,
                    DiagnosisId = model.DiagnosisId,
                    HeightCm = model.HeightCm,
                    WeightKg = model.WeightKg,
                    HeartBeatRateBpm = model.HeartBeatRateBpm,
                    TemperatureC = model.TemperatureC,
                    HealthProfessionalId = model.HealthProfessionalId,
                    Observations = model.Observations,
                    ReferredTo = model.ReferredTo,
                    NextAppointmentDate = model.NextAppointmentDate,
                };

                Context.EvolutionCards.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.EvolutionCardId;
            }
        }
    }
}