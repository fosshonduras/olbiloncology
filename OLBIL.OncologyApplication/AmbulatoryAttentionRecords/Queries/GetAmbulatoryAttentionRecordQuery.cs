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

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Queries
{
    public class GetAmbulatoryAttentionRecordQuery : IRequest<AmbulatoryAttentionRecordModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetAmbulatoryAttentionRecordQuery, AmbulatoryAttentionRecordModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<AmbulatoryAttentionRecordModel> Handle(GetAmbulatoryAttentionRecordQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<AmbulatoryAttentionRecordModel>(await Context
                    .AmbulatoryAttentionRecords.Where(o => o.AmbulatoryAttentionRecordId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(AmbulatoryAttentionRecord), nameof(item.AmbulatoryAttentionRecordId), request.Id);
                }

                return item;
            }
        }
    }
}