using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class GetCountriesListQuery : IRequest<ListModel<CountryModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetCountriesListQuery, ListModel<CountryModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<CountryModel>> Handle(GetCountriesListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<CountryModel>
                {
                    Items = await Context.Countries
                                       .ProjectTo<CountryModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
