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

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Commands
{
    public class CreateTransfusionProductDetailCommand : IRequest<int>
    {
        public TransfusionProductDetailModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateTransfusionProductDetailCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateTransfusionProductDetailCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.TransfusionProductDetails
                    .Where(p => p.TransfusionProductDetailId == model.TransfusionProductDetailId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(TransfusionProductDetail), nameof(model.TransfusionProductDetailId), model.TransfusionProductDetailId);
                }

                var newRecord = new TransfusionProductDetail
                {
                    BloodTransfusionId = model.BloodTransfusionId.Value,
                    UnitNumber = model.UnitNumber,
                    Component = model.Component,
                    ABORH = model.ABORH,
                    Quantity = model.Quantity,
                    StartTime = model.StartTime.Value,
                    EndTime = model.EndTime,
                    Responsible = model.Responsible,
                    AdverseReactions = model.AdverseReactions,
                };

                Context.TransfusionProductDetails.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.TransfusionProductDetailId;
            }
        }
    }
}