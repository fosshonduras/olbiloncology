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

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardQuery : IRequest<WardModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetWardQuery, WardModel>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<WardModel> Handle(GetWardQuery request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<WardModel>(await _context
                    .Wards.Where(o => o.WardId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Ward), nameof(item.WardId), request.Id);
                }

                return item;
            }
        }
    }
}