using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class GetCountriesListQuery : GetListBase, IRequest<ListModel<CountryModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetCountriesListQuery, ListModel<CountryModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<CountryModel>> Handle(GetCountriesListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<Country>(i => i.CountryId);

                return await RetrieveListResults<Country, CountryModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
