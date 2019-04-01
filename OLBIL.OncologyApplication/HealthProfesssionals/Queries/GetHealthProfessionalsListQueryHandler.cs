using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class GetHealthProfessionalsListQueryHandler : IRequestHandler<GetHealthProfessionalsListQuery, HealthProfessionalsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetHealthProfessionalsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HealthProfessionalsListModel> Handle(GetHealthProfessionalsListQuery request, CancellationToken cancellationToken)
        {
            return new HealthProfessionalsListModel
            {
                Items = await _context.HealthProfessionals.Include(o => o.Person)
                                    .ProjectTo<HealthProfessionalModel>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken)
            };
        }
    }
}