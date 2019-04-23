using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyDomain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Appointments.Commands
{
    public class UpdateAppointmentCommand : IRequest
    {
        public AppointmentModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateAppointmentCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Appointments
                    .Where(p => p.AppointmentId == model.AppointmentId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Appointment), nameof(model.AppointmentId), model.AppointmentId);
                }

                item.AppointmentStatusId = (AppointmentStatus)model.AppointmentStatusId;
                item.HealthProfessionalId = model.HealthProfessionalId;
                item.Notes = model.Notes;
                item.SpecialNotes = model.SpecialNotes;
                item.RescheduledAppointmentId = item.RescheduledAppointmentId;
                item.PatientAttended = item.PatientAttended;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
