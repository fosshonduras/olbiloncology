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
    public class UpdateCountryCommand : IRequest
    {
        public CountryModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateCountryCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.Countries
                    .Where(p => p.CountryId == model.CountryId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(Country), nameof(model.CountryId), model.CountryId);
                }

                item.ISOCode2 = model.ISOCode2;
                item.ISOCode3 = model.ISOCode3;
                item.NameEn = model.NameEn;
                item.NameEs = model.NameEs;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
