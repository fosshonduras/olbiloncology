using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.RecordStorageLocations.Queries
{
    public class SearchRecordStorageLocationsQuery : SearchBase, IRequest<ListModel<RecordStorageLocationModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchRecordStorageLocationsQuery, ListModel<RecordStorageLocationModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<ListModel<RecordStorageLocationModel>> Handle(SearchRecordStorageLocationsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<RecordStorageLocation, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<RecordStorageLocation, RecordStorageLocationModel>(predicate, request, cancellationToken);
            }
        }
    }
}
