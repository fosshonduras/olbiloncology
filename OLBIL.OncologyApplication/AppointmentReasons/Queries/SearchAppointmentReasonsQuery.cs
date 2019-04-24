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

namespace OLBIL.OncologyApplication.AppointmentReasons.Queries
{
    public class SearchAppointmentReasonsQuery : SearchBase, IRequest<ListModel<AppointmentReasonModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchAppointmentReasonsQuery, ListModel<AppointmentReasonModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentReasonModel>> Handle(SearchAppointmentReasonsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<AppointmentReason, bool>> predicate = i => EF.Functions.ILike(i.Description, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<AppointmentReason, AppointmentReasonModel>(predicate, request, cancellationToken);
            }
        }
    }
}
