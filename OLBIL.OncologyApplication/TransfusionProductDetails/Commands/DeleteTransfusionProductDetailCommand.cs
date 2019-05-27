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

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Commands
{
    public class DeleteTransfusionProductDetailCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteTransfusionProductDetailCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(DeleteTransfusionProductDetailCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.TransfusionProductDetails
                    .Where(p => p.TransfusionProductDetailId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionProductDetail), nameof(item.TransfusionProductDetailId), request.Id);
                }

                Context.TransfusionProductDetails.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}