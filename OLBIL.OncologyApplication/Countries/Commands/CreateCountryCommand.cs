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

namespace OLBIL.OncologyApplication.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public CountryModel Model { get; set; }

        public class Handler : IRequestHandler<CreateCountryCommand, int>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var ward = await _context.Countries
                    .Where(p => p.CountryId == model.CountryId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (ward != null)
                {
                    throw new AlreadyExistsException(nameof(Country), nameof(model.CountryId), model.CountryId);
                }

                var newRecord = new Country
                {
                    ISOCode2 = model.ISOCode2,
                    ISOCode3 = model.ISOCode3,
                    NameEn = model.NameEn,
                    NameEs = model.NameEs
                };

                _context.Countries.Add(newRecord);
                await _context.SaveChangesAsync(cancellationToken);

                return newRecord.CountryId;
            }
        }
    }
}