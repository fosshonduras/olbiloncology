using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Diagnosiss.Queries
{
    public class SearchDiagnosesQuery : IRequest<ListModel<DiagnosisModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchDiagnosesQuery, ListModel<DiagnosisModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<DiagnosisModel>> Handle(SearchDiagnosesQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<DiagnosisModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<DiagnosisModel>> ApplyFilter(SearchDiagnosesQuery request, CancellationToken cancellationToken)
            {
                return await Context.Diagnoses
                                    .Where(i =>
                                        EF.Functions.ILike(i.ICDCode, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.CompleteDescriptor, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.ShortDescriptor, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<DiagnosisModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
