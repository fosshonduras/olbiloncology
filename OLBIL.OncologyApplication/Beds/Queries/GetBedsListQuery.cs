using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedsListQuery: GetListBase, IRequest<ListModel<BedModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetBedsListQuery, ListModel<BedModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BedModel>> Handle(GetBedsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BedModel>
                {
                    Items = await Context.Beds
                                       .ProjectTo<BedModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
