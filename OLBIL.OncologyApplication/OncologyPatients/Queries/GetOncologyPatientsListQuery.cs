using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientsListQuery: GetListBase, IRequest<ListModel<OncologyPatientModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetOncologyPatientsListQuery, ListModel<OncologyPatientModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<OncologyPatientModel>> Handle(GetOncologyPatientsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<OncologyPatientModel>
                {
                    Items = await Context.OncologyPatients.Include(o => o.Person)
                                        .ProjectTo<OncologyPatientModel>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
