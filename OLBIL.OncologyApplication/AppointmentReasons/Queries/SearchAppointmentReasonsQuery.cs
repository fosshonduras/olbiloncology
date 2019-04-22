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

namespace OLBIL.OncologyApplication.AppointmentReasons.Queries
{
    public class SearchAppointmentReasonsQuery : IRequest<ListModel<AppointmentReasonModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchAppointmentReasonsQuery, ListModel<AppointmentReasonModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AppointmentReasonModel>> Handle(SearchAppointmentReasonsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AppointmentReasonModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<AppointmentReasonModel>> ApplyFilter(SearchAppointmentReasonsQuery request, CancellationToken cancellationToken)
            {
                return await Context.AppointmentReasons
                                    .Where(i =>
                                        EF.Functions.ILike(i.Description, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<AppointmentReasonModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
