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

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Queries
{
    public class GetAmbulatoryAttentionRecordsListQuery : GetListBase, IRequest<ListModel<AmbulatoryAttentionRecordModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetAmbulatoryAttentionRecordsListQuery, ListModel<AmbulatoryAttentionRecordModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AmbulatoryAttentionRecordModel>> Handle(GetAmbulatoryAttentionRecordsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<AmbulatoryAttentionRecord>(i => i.AmbulatoryAttentionRecordId);
                return await RetrieveListResults<AmbulatoryAttentionRecord, AmbulatoryAttentionRecordModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
