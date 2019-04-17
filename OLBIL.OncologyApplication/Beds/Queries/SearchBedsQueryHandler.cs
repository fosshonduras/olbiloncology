using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class SearchBedsListQueryHandler : IRequestHandler<SearchBedsQuery, ListModel<BedModel>>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchBedsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListModel<BedModel>> Handle(SearchBedsQuery request, CancellationToken cancellationToken)
        {
            return new ListModel<BedModel>
            {
                Items = await _context.Beds
                                   .Where(i =>
                                        EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.LongDescription, $"%{request.SearchTerm}%")
                                    )
                                   .ProjectTo<BedModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
