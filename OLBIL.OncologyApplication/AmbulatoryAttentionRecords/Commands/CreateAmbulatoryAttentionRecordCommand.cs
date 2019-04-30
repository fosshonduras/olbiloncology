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
using OLBIL.Common;

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Commands
{
    public class CreateAmbulatoryAttentionRecordCommand : IRequest<int>
    {
        public AmbulatoryAttentionRecordModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateAmbulatoryAttentionRecordCommand, int>
        {
            private readonly IDateTimeProvider _dateTimeProvider;

            public Handler(IOncologyContext context, IMapper mapper, IDateTimeProvider dateTimeProvider) : base(context, mapper) {

                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<int> Handle(CreateAmbulatoryAttentionRecordCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.AmbulatoryAttentionRecords
                    .Where(p => p.AmbulatoryAttentionRecordId == model.AmbulatoryAttentionRecordId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(AmbulatoryAttentionRecord), nameof(model.AmbulatoryAttentionRecordId), model.AmbulatoryAttentionRecordId);
                }

                var newRecord = new AmbulatoryAttentionRecord
                {
                    HealthProfessionalId = model.HealthProfessionalId.Value,
                    OncologyPatientId = model.OncologyPatientId.Value,
                    DiagnosisId = model.DiagnosisId.Value,
                    IsNewPatient = model.IsNewPatient,
                    Date = _dateTimeProvider.Now,
                    NextAppointmentDate = model.NextAppointmentDate,
                    ReceivedFrom = model.ReceivedFrom,
                    ReferredTo = model.ReferredTo,
                    TreatmentPhase = model.TreatmentPhase,
                    DiseaseEventDescription = model.DiseaseEventDescription,
                };

                Context.AmbulatoryAttentionRecords.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.AmbulatoryAttentionRecordId;
            }
        }
    }
}