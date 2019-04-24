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

namespace OLBIL.OncologyApplication.HealthProfessionals.Queries
{
    public class SearchHealthProfessionalsQuery : SearchBase, IRequest<ListModel<HealthProfessionalModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchHealthProfessionalsQuery, ListModel<HealthProfessionalModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HealthProfessionalModel>> Handle(SearchHealthProfessionalsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<HealthProfessional, bool>> predicate = i => EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<HealthProfessional, HealthProfessionalModel>(predicate, request, cancellationToken);
            }
        }
    }
}
