using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
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

        public class Handler : IRequestHandler<GetBedQuery, BedModel>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BedModel> Handle(GetBedQuery request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<BedModel>(await _context
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
