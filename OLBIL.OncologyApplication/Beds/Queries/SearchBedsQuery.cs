using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class SearchBedsQuery : IRequest<ListModel<BedModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchBedsQuery, ListModel<BedModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<ListModel<BedModel>> Handle(SearchBedsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BedModel>
                {
                    Items = await Context.Beds
                                       .Where(i =>
                                            EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.LongDescription, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<BedModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
