using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedQueryHandler : IRequestHandler<GetBedQuery, BedModel>
    {        
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetBedQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BedModel> Handle(GetBedQuery request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<BedModel>(await _context
                .Beds.Where(o => o.BedId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if(item == null)
            {
                throw new NotFoundException(nameof(Bed), nameof(item.BedId), request.Id);
            }

            return item;
        }
    }
}
