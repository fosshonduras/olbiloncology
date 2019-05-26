using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Appointments.Queries
{
    public class GetAppointmentsListQuery : GetListBase, IRequest<ListModel<AppointmentModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetAppointmentsListQuery, ListModel<AppointmentModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentModel>> Handle(GetAppointmentsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<Appointment>(i => i.AppointmentId);

                return await RetrieveListResults<Appointment, AppointmentModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}