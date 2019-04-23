using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Appointments.Queries
{
    public class GetAppointmentsListQuery : GetListBase, IRequest<ListModel<AppointmentModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetAppointmentsListQuery, ListModel<AppointmentModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentModel>> Handle(GetAppointmentsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AppointmentModel>
                {
                    Items = await Context.Appointments
                                       .ProjectTo<AppointmentModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}