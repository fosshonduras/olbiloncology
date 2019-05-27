
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Queries
{
    public class GetTransfusionProductDetailQuery : IRequest<TransfusionProductDetailModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetTransfusionProductDetailQuery, TransfusionProductDetailModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<TransfusionProductDetailModel> Handle(GetTransfusionProductDetailQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<TransfusionProductDetailModel>(await Context
                    .TransfusionProductDetails.Where(o => o.TransfusionProductDetailId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionProductDetail), nameof(item.TransfusionProductDetailId), request.Id);
                }

                return item;
            }
        }
    }
}