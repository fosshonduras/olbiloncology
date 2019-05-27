using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Commands
{
    public class DeleteTransfusionVitalSignsDetailCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteTransfusionVitalSignsDetailCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(DeleteTransfusionVitalSignsDetailCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.TransfusionVitalSignsDetails
                    .Where(p => p.TransfusionVitalSignsDetailId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionVitalSignsDetail), nameof(item.TransfusionVitalSignsDetailId), request.Id);
                }

                Context.TransfusionVitalSignsDetails.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}