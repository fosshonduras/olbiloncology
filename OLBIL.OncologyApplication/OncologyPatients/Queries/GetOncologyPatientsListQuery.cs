using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientsListQuery: GetListBase, IRequest<ListModel<OncologyPatientModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetOncologyPatientsListQuery, ListModel<OncologyPatientModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<OncologyPatientModel>> Handle(GetOncologyPatientsListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<OncologyPatient, OncologyPatientModel>(null, request, cancellationToken);
            }
        }
    }
}
