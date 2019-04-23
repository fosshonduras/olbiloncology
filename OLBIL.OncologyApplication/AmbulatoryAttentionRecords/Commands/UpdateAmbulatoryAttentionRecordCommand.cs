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

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Commands
{
    public class UpdateAmbulatoryAttentionRecordCommand : IRequest
    {
        public AmbulatoryAttentionRecordModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateAmbulatoryAttentionRecordCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateAmbulatoryAttentionRecordCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.AmbulatoryAttentionRecords
                    .Where(p => p.AmbulatoryAttentionRecordId == model.AmbulatoryAttentionRecordId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(AmbulatoryAttentionRecord), nameof(model.AmbulatoryAttentionRecordId), model.AmbulatoryAttentionRecordId);
                }

                item.HealthProfessionalId = model.HealthProfessionalId.Value;
                item.OncologyPatientId = model.OncologyPatientId.Value;
                item.DiagnosisId = model.DiagnosisId.Value;
                item.IsNewPatient = model.IsNewPatient;

                item.NextAppointmentDate = model.NextAppointmentDate;
                item.ReceivedFrom = model.ReceivedFrom;
                item.ReferredTo = model.ReferredTo;
                item.TreatmentPhase = model.TreatmentPhase;
                item.DiseaseEventDescription = model.DiseaseEventDescription;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}