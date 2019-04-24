using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Appointments.Queries
{
    public class SearchAppointmentsQuery : SearchBase, IRequest<ListModel<AppointmentModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchAppointmentsQuery, ListModel<AppointmentModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentModel>> Handle(SearchAppointmentsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Appointment, bool>> predicate = i => EF.Functions.ILike(i.Notes, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.SpecialNotes, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<Appointment, AppointmentModel>(predicate, request, cancellationToken);
            }
        }
    }
}
