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

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Commands
{
    public class UpdateTransfusionProductDetailCommand : IRequest
    {
        public TransfusionProductDetailModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateTransfusionProductDetailCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateTransfusionProductDetailCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionProductDetails
                    .Where(p => p.TransfusionProductDetailId == model.TransfusionProductDetailId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionProductDetail), nameof(model.TransfusionProductDetailId), model.TransfusionProductDetailId);
                }

                item.BloodTransfusionId = model.BloodTransfusionId.Value;
                item.UnitNumber = model.UnitNumber;
                item.Component = model.Component;
                item.ABORH = model.ABORH;
                item.Quantity = model.Quantity;
                item.StartTime = model.StartTime.Value;
                item.EndTime = model.EndTime;
                item.Responsible = model.Responsible;
                item.AdverseReactions = model.AdverseReactions;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}