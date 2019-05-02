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

namespace OLBIL.OncologyApplication.Appointments.Commands
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public AppointmentModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateAppointmentCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var ward = await Context.Appointments
                    .Where(p => p.AppointmentId == model.AppointmentId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (ward != null)
                {
                    throw new AlreadyExistsException(nameof(Appointment), nameof(model.AppointmentId), model.AppointmentId);
                }

                var newRecord = new Appointment
                {
                    AppointmentReasonId = model.AppointmentReasonId,
                    AttentionBlocks = model.AttentionBlocks,
                    Date = model.Date.Value,
                    OncologyPatientId = model.OncologyPatientId.Value,
                    HealthProfessionalId = model.HealthProfessionalId,
                    PatientAttended = model.PatientAttended,
                    AppointmentStatusId = model.AppointmentStatusId.Value,
                    Notes = model.Notes,
                    SpecialNotes = model.SpecialNotes,
                    RescheduledAppointmentId = model.RescheduledAppointmentId.Value
                };

                Context.Appointments.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.AppointmentId;
            }
        }
    }
}