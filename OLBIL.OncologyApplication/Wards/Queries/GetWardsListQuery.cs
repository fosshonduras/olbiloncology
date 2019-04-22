using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AppointmentReasons.Queries
{
    public class GetAppointmentReasonsListQuery: IRequest<ListModel<AppointmentReasonModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetAppointmentReasonsListQuery, ListModel<AppointmentReasonModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentReasonModel>> Handle(GetAppointmentReasonsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AppointmentReasonModel>
                {
                    Items = await Context.AppointmentReasons
                                       .ProjectTo<AppointmentReasonModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
