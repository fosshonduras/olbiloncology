using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;

namespace OLBIL.OncologyApplication.Diagnosiss.Queries
{
    public class GetDiagnosisQuery : IRequest<DiagnosisModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetDiagnosisQuery, DiagnosisModel>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<DiagnosisModel> Handle(GetDiagnosisQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<DiagnosisModel>(await Context
                    .Diagnoses.Where(o => o.DiagnosisId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Diagnosis), nameof(item.DiagnosisId), request.Id);
                }

                return item;
            }
        }
    }
}