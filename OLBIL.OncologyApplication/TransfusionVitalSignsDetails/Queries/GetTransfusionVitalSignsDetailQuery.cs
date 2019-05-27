
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

namespace OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Queries
{
    public class GetTransfusionVitalSignsDetailQuery : IRequest<TransfusionVitalSignsDetailModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetTransfusionVitalSignsDetailQuery, TransfusionVitalSignsDetailModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<TransfusionVitalSignsDetailModel> Handle(GetTransfusionVitalSignsDetailQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<TransfusionVitalSignsDetailModel>(await Context
                    .TransfusionVitalSignsDetails.Where(o => o.TransfusionVitalSignsDetailId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(TransfusionVitalSignsDetail), nameof(item.TransfusionVitalSignsDetailId), request.Id);
                }

                return item;
            }
        }
    }
}