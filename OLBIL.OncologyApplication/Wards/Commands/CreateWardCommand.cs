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

namespace OLBIL.OncologyApplication.AppointmentReasons.Commands
{
    public class CreateAppointmentReasonCommand : IRequest<int>
    {
        public AppointmentReasonModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateAppointmentReasonCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateAppointmentReasonCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var ward = await Context.AppointmentReasons
                    .Where(p => p.AppointmentReasonId == model.AppointmentReasonId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (ward != null)
                {
                    throw new AlreadyExistsException(nameof(AppointmentReason), nameof(model.AppointmentReasonId), model.AppointmentReasonId);
                }

                var newRecord = new AppointmentReason
                {
                    Description = model.Description
                };

                Context.AppointmentReasons.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.AppointmentReasonId;
            }
        }
    }
}