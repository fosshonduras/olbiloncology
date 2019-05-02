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

namespace OLBIL.OncologyApplication.PhysicalRecordTransfers.Queries
{
    public class GetPhysicalRecordTransferQuery: IRequest<PhysicalRecordTransferModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetPhysicalRecordTransferQuery, PhysicalRecordTransferModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<PhysicalRecordTransferModel> Handle(GetPhysicalRecordTransferQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<PhysicalRecordTransferModel>(await Context
                    .PhysicalRecordTransfers.Where(o => o.PhysicalRecordTransferId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(PhysicalRecordTransfer), nameof(item.PhysicalRecordTransferId), request.Id);
                }

                return item;
            }
        }
    }
}
