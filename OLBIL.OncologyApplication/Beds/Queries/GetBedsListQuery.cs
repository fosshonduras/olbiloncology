using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Infrastructure.EF;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedsListQuery: GetListBase, IRequest<ListModel<BedModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetBedsListQuery, ListModel<BedModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BedModel>> Handle(GetBedsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<Bed>(i => i.BedId);

                return await RetrieveListResults<Bed, BedModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
