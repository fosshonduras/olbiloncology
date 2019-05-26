using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Infrastructure.EF;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AppointmentReasons.Queries
{
    public class GetAppointmentReasonsListQuery : GetListBase, IRequest<ListModel<AppointmentReasonModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetAppointmentReasonsListQuery, ListModel<AppointmentReasonModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentReasonModel>> Handle(GetAppointmentReasonsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<AppointmentReason>(i => i.AppointmentReasonId);

                return await RetrieveListResults<AppointmentReason, AppointmentReasonModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}