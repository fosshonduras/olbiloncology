using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class GetCountryQuery : IRequest<CountryModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetCountryQuery, CountryModel>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CountryModel> Handle(GetCountryQuery request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<CountryModel>(await _context
                    .Countries.Where(o => o.CountryId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Country), nameof(item.CountryId), request.Id);
                }

                return item;
            }
        }
    }
}