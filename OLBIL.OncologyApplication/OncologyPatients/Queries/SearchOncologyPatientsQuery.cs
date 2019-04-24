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

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class SearchOncologyPatientsQuery : SearchBase, IRequest<ListModel<OncologyPatientModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchOncologyPatientsQuery, ListModel<OncologyPatientModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<OncologyPatientModel>> Handle(SearchOncologyPatientsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<OncologyPatient, bool>> predicate = i =>
                                          EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                          || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                          || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                          || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                          || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                          || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<OncologyPatient, OncologyPatientModel>(predicate, request, cancellationToken);
            }
        }
    }
}
