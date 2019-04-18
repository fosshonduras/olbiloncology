using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class SearchCountriesQuery : IRequest<ListModel<CountryModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchCountriesQuery, ListModel<CountryModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<CountryModel>> Handle(SearchCountriesQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<CountryModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<CountryModel>> ApplyFilter(SearchCountriesQuery request, CancellationToken cancellationToken)
            {
                return await Context.Countries
                                    .Where(i =>
                                        EF.Functions.ILike(i.NameEn, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.NameEs, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.ISOCode2, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.ISOCode3, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<CountryModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
