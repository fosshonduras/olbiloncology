using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class SearchWardsListQueryHandler : IRequestHandler<SearchWardsQuery, ListModel<WardModel>>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchWardsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListModel<WardModel>> Handle(SearchWardsQuery request, CancellationToken cancellationToken)
        {
            return new ListModel<WardModel>
            {
                Items = await ApplyFilter(request, cancellationToken)
            };
        }

        private async Task<List<WardModel>> ApplyFilter(SearchWardsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Wards
                                .Where(i =>
                                    EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                )
                                .ProjectTo<WardModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);
        }
    }
}
