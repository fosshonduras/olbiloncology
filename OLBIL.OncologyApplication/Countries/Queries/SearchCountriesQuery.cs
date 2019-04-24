using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Countries.Queries
{
    public class SearchCountriesQuery : SearchBase, IRequest<ListModel<CountryModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchCountriesQuery, ListModel<CountryModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<CountryModel>> Handle(SearchCountriesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<CountryModel, bool>> predicate = i => EF.Functions.ILike(i.NameEn, $"%{request.SearchTerm}%")
                                     || EF.Functions.ILike(i.NameEs, $"%{request.SearchTerm}%")
                                     || EF.Functions.ILike(i.ISOCode2, $"%{request.SearchTerm}%")
                                     || EF.Functions.ILike(i.ISOCode3, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<CountryModel, CountryModel>(predicate, request, cancellationToken);
            }
        }
    }
}
