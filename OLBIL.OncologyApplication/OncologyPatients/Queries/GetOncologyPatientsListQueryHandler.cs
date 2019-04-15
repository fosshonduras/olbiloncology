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
    public class GetOncologyPatientsListQueryHandler : IRequestHandler<GetOncologyPatientsListQuery, OncologyPatientsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetOncologyPatientsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OncologyPatientsListModel> Handle(GetOncologyPatientsListQuery request, CancellationToken cancellationToken)
        {
            return new OncologyPatientsListModel
            {
                Items = await _context.OncologyPatients.Include(o => o.Person)
                                    .ProjectTo<OncologyPatientModel>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken)
            };
        }
    }
}
