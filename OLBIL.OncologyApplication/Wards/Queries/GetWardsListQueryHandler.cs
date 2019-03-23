using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Queries
{
       public class GetWardsListQueryHandler : IRequestHandler<GetWardsListQuery, WardsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetWardsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WardsListModel> Handle(GetWardsListQuery request, CancellationToken cancellationToken)
        {
            return new WardsListModel
            {
                Items = await _context.Wards
                                   .ProjectTo<WardModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
