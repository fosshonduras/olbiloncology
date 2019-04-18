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

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class DeleteWardCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteWardCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(DeleteWardCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.Wards
                    .Where(p => p.WardId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Ward), nameof(item.WardId), request.Id);
                }

                Context.Wards.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
