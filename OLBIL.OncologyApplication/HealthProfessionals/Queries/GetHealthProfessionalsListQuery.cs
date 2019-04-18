using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfessionals.Queries
{
    public class GetHealthProfessionalsListQuery: IRequest<ListModel<HealthProfessionalModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetHealthProfessionalsListQuery, ListModel<HealthProfessionalModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HealthProfessionalModel>> Handle(GetHealthProfessionalsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<HealthProfessionalModel>
                {
                    Items = await Context.HealthProfessionals.Include(o => o.Person)
                                        .ProjectTo<HealthProfessionalModel>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
