using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Infrastructure.EF;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Queries
{
    public class GetAdministrativeDivisionsListQuery : GetListBase, IRequest<ListModel<AdministrativeDivisionModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetAdministrativeDivisionsListQuery, ListModel<AdministrativeDivisionModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AdministrativeDivisionModel>> Handle(GetAdministrativeDivisionsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<AdministrativeDivision>(i => i.AdministrativeDivisionId);

                return await RetrieveListResults<AdministrativeDivision, AdministrativeDivisionModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
