
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

namespace OLBIL.OncologyApplication.BloodTransfusions.Queries
{
    public class GetBloodTransfusionQuery : IRequest<BloodTransfusionModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetBloodTransfusionQuery, BloodTransfusionModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<BloodTransfusionModel> Handle(GetBloodTransfusionQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<BloodTransfusionModel>(await Context
                    .BloodTransfusions.Where(o => o.BloodTransfusionId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(BloodTransfusion), nameof(item.BloodTransfusionId), request.Id);
                }

                return item;
            }
        }
    }
}