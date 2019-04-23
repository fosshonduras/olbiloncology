using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.Appointments.Queries
{
    public class GetAppointmentQuery : IRequest<AppointmentModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetAppointmentQuery, AppointmentModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<AppointmentModel> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<AppointmentModel>(await Context
                    .Appointments.Where(o => o.AppointmentId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Appointment), nameof(item.AppointmentId), request.Id);
                }

                return item;
            }
        }
    }
}