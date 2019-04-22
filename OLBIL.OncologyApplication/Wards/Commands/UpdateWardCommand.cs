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

namespace OLBIL.OncologyApplication.AppointmentReasons.Commands
{
    public class UpdateAppointmentReasonCommand: IRequest
    {
        public AppointmentReasonModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateAppointmentReasonCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateAppointmentReasonCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.AppointmentReasons
                    .Where(p => p.AppointmentReasonId == model.AppointmentReasonId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(AppointmentReason), nameof(model.AppointmentReasonId), model.AppointmentReasonId);
                }

                item.Description = model.Description;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
