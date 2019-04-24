using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfessionals.Queries
{
    public class GetHealthProfessionalsListQuery: GetListBase, IRequest<ListModel<HealthProfessionalModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetHealthProfessionalsListQuery, ListModel<HealthProfessionalModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HealthProfessionalModel>> Handle(GetHealthProfessionalsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<HealthProfessional, HealthProfessionalModel>(null, request, cancellationToken);
            }
        }
    }
}
