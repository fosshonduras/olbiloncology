using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardsListQuery: IRequest<ListModel<WardModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetWardsListQuery, ListModel<WardModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<WardModel>> Handle(GetWardsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<WardModel>
                {
                    Items = await Context.Wards
                                       .ProjectTo<WardModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
