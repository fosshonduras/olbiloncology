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

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class GetCountryQuery : IRequest<CountryModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetCountryQuery, CountryModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<CountryModel> Handle(GetCountryQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<CountryModel>(await Context
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