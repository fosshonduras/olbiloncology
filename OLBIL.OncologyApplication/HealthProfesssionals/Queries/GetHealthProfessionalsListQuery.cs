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
    public class GetHealthProfessionalsListQuery: IRequest<ListModel<HealthProfessionalModel>>
    {
        public class Handler : IRequestHandler<GetHealthProfessionalsListQuery, ListModel<HealthProfessionalModel>>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListModel<HealthProfessionalModel>> Handle(GetHealthProfessionalsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<HealthProfessionalModel>
                {
                    Items = await _context.HealthProfessionals.Include(o => o.Person)
                                        .ProjectTo<HealthProfessionalModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
