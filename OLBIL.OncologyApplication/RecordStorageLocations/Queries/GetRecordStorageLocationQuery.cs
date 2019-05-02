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

namespace OLBIL.OncologyApplication.RecordStorageLocations.Queries
{
    public class GetRecordStorageLocationQuery: IRequest<RecordStorageLocationModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetRecordStorageLocationQuery, RecordStorageLocationModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<RecordStorageLocationModel> Handle(GetRecordStorageLocationQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<RecordStorageLocationModel>(await Context
                    .RecordStorageLocations.Where(o => o.RecordStorageLocationId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(RecordStorageLocation), nameof(item.RecordStorageLocationId), request.Id);
                }

                return item;
            }
        }
    }
}
