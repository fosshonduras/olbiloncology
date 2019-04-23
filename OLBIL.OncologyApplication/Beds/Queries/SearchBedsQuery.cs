using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class SearchBedsQuery : SearchBase, IRequest<ListModel<BedModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<SearchBedsQuery, ListModel<BedModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
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
