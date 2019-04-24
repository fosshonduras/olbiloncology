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

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class SearchBuildingsQuery : SearchBase, IRequest<ListModel<BuildingModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchBuildingsQuery, ListModel<BuildingModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BuildingModel>> Handle(SearchBuildingsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Building, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<Building, BuildingModel>(predicate, request, cancellationToken);
            }
        }
    }
}
