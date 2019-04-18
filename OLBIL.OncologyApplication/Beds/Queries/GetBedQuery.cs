using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedQuery: IRequest<BedModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetBedQuery, BedModel>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<BedModel> Handle(GetBedQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<BedModel>(await Context
                    .Beds.Where(o => o.BedId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Bed), nameof(item.BedId), request.Id);
                }

                return item;
            }
        }
    }
}
