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

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Queries
{
    public class SearchAdministrativeDivisionsQuery : IRequest<ListModel<AdministrativeDivisionModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchAdministrativeDivisionsQuery, ListModel<AdministrativeDivisionModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AdministrativeDivisionModel>> Handle(SearchAdministrativeDivisionsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AdministrativeDivisionModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<AdministrativeDivisionModel>> ApplyFilter(SearchAdministrativeDivisionsQuery request, CancellationToken cancellationToken)
            {
                return await Context.AdministrativeDivisions
                                    .Where(i =>
                                        EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<AdministrativeDivisionModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
