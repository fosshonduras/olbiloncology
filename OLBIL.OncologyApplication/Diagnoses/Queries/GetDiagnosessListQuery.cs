using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Diagnosiss.Queries
{
    public class GetDiagnosesListQuery: IRequest<ListModel<DiagnosisModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetDiagnosesListQuery, ListModel<DiagnosisModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<DiagnosisModel>> Handle(GetDiagnosesListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<DiagnosisModel>
                {
                    Items = await Context.Diagnoses
                                       .ProjectTo<DiagnosisModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
