using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Diagnosiss.Queries
{
    public class GetDiagnosesListQuery: GetListBase, IRequest<ListModel<DiagnosisModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetDiagnosesListQuery, ListModel<DiagnosisModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<DiagnosisModel>> Handle(GetDiagnosesListQuery request, CancellationToken cancellationToken)
            {
                return await RetrieveListResults<Diagnosis, DiagnosisModel>(null, request, cancellationToken);
            }
        }
    }
}
