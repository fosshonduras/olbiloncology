using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Diagnosiss.Commands
{
    public class DeleteDiagnosisCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteDiagnosisCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.Diagnoses
                    .Where(p => p.DiagnosisId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Diagnosis), nameof(item.DiagnosisId), request.Id);
                }

                Context.Diagnoses.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
