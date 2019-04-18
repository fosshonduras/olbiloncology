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

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Commands
{
    public class DeleteAdministrativeDivisionCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteAdministrativeDivisionCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteAdministrativeDivisionCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.AdministrativeDivisions
                    .Where(p => p.AdministrativeDivisionId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(AdministrativeDivision), nameof(item.AdministrativeDivisionId), request.Id);
                }

                Context.AdministrativeDivisions.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
