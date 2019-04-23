using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
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
                return await RetrieveListResults<AmbulatoryAttentionRecord, AmbulatoryAttentionRecordModel>(null, request, cancellationToken);
            }
        }
    }
}
