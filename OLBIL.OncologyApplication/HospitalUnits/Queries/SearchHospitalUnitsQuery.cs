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

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class SearchHospitalUnitsQuery : SearchBase, IRequest<ListModel<HospitalUnitModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchHospitalUnitsQuery, ListModel<HospitalUnitModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HospitalUnitModel>> Handle(SearchHospitalUnitsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<HospitalUnit, bool>> predicate = i => EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<HospitalUnit, HospitalUnitModel>(predicate, request, cancellationToken);
            }
        }
    }
}
