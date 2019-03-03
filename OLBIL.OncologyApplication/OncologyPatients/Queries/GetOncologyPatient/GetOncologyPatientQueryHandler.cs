using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatient
{
    public class GetOncologyPatientQueryHandler : IRequestHandler<GetOncologyPatientQuery, OncologyPatientModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetOncologyPatientQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OncologyPatientModel> Handle(GetOncologyPatientQuery request, CancellationToken cancellationToken)
        {
            
            var item = _mapper.Map<OncologyPatientModel>(await _context
                .OncologyPatients.Include(o => o.Person).Where(o => o.OncologyPatientId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));
            if(item == null)
            {
                throw new NotFoundException(nameof(OncologyPatient), request.Id);
            }
            return item;
        }
    }
}
