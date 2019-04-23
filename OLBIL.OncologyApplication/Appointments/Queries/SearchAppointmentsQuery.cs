using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Appointments.Queries
{
    public class SearchAppointmentsQuery : SearchBase, IRequest<ListModel<AppointmentModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<SearchAppointmentsQuery, ListModel<AppointmentModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentModel>> Handle(SearchAppointmentsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AppointmentModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<AppointmentModel>> ApplyFilter(SearchAppointmentsQuery request, CancellationToken cancellationToken)
            {
                return await Context.Appointments
                                    .Where(i =>
                                        EF.Functions.ILike(i.Notes, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.SpecialNotes, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<AppointmentModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
