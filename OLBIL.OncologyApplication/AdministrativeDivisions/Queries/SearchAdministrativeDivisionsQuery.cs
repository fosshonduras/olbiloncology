﻿using AutoMapper;
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

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Queries
{
    public class SearchAdministrativeDivisionsQuery : SearchBase, IRequest<ListModel<AdministrativeDivisionModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchAdministrativeDivisionsQuery, ListModel<AdministrativeDivisionModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AdministrativeDivisionModel>> Handle(SearchAdministrativeDivisionsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<AdministrativeDivision, bool>> predicate = i =>
                                        EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<AdministrativeDivision>(i => i.AdministrativeDivisionId);

                return await RetrieveSearchResults<AdministrativeDivision, AdministrativeDivisionModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
