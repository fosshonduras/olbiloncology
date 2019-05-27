using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public CountryModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateCountryCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Countries
                    .Where(p => p.CountryId == model.CountryId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
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

                Context.Countries.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.CountryId;
            }
        }
    }
}