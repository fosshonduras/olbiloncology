using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Diagnosiss.Commands
{
    public class UpdateDiagnosisCommand: IRequest
    {
        public DiagnosisModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateDiagnosisCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Diagnoses
                    .Where(p => p.DiagnosisId == model.DiagnosisId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Diagnosis), nameof(model.DiagnosisId), model.DiagnosisId);
                }

                item.ICDCode = model.ICDCode;
                item.ShortDescriptor = model.ShortDescriptor;
                item.CompleteDescriptor = model.CompleteDescriptor;
                
                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
