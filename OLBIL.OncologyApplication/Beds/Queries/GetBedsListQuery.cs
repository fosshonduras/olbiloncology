using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedsListQuery: IRequest<ListModel<BedModel>>
    {
        public class Handler : IRequestHandler<GetBedsListQuery, ListModel<BedModel>>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListModel<BedModel>> Handle(GetBedsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BedModel>
                {
                    Items = await _context.Beds
                                       .ProjectTo<BedModel>(_mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
