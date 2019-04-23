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

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Commands
{
    public class DeleteAmbulatoryAttentionRecordCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteAmbulatoryAttentionRecordCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(DeleteAmbulatoryAttentionRecordCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.AmbulatoryAttentionRecords
                    .Where(p => p.AmbulatoryAttentionRecordId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(AmbulatoryAttentionRecord), nameof(item.AmbulatoryAttentionRecordId), request.Id);
                }

                Context.AmbulatoryAttentionRecords.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}