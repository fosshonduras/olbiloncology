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

namespace OLBIL.OncologyApplication.HealthProfessionals.Commands
{
    public class DeleteHealthProfessionalCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteHealthProfessionalCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteHealthProfessionalCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.HealthProfessionals
                    .Where(p => p.HealthProfessionalId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HealthProfessional), nameof(item.HealthProfessionalId), request.Id);
                }

                Context.HealthProfessionals.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
