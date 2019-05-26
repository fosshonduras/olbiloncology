using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.RecordStorageLocations.Queries
{
    public class GetRecordStorageLocationsListQuery: GetListBase, IRequest<ListModel<RecordStorageLocationModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetRecordStorageLocationsListQuery, ListModel<RecordStorageLocationModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<RecordStorageLocationModel>> Handle(GetRecordStorageLocationsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<RecordStorageLocation>(i => i.RecordStorageLocationId);

                return await RetrieveListResults<RecordStorageLocation, RecordStorageLocationModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
