using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.Diagnosiss.Commands
{
    public class CreateDiagnosisCommand : IRequest<int>
    {
        public DiagnosisModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateDiagnosisCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Diagnoses
                    .Where(p => p.DiagnosisId == model.DiagnosisId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(Diagnosis), nameof(model.DiagnosisId), model.DiagnosisId);
                }

                var newRecord = new Diagnosis
                {
                    ICDCode = model.ICDCode,
                    CompleteDescriptor = model.CompleteDescriptor,
                    ShortDescriptor = model.ShortDescriptor
                };

                Context.Diagnoses.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.DiagnosisId;
            }
        }
    }
}