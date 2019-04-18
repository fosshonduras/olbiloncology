using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientsListQuery: IRequest<ListModel<OncologyPatientModel>>
    {
        public class Handler : IRequestHandler<GetOncologyPatientsListQuery, ListModel<OncologyPatientModel>>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListModel<OncologyPatientModel>> Handle(GetOncologyPatientsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<OncologyPatientModel>
                {
                    Items = await _context.OncologyPatients.Include(o => o.Person)
                                        .ProjectTo<OncologyPatientModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
