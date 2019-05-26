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

namespace OLBIL.OncologyApplication.Diagnosiss.Queries
{
    public class SearchDiagnosesQuery : SearchBase, IRequest<ListModel<DiagnosisModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchDiagnosesQuery, ListModel<DiagnosisModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<DiagnosisModel>> Handle(SearchDiagnosesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Diagnosis, bool>> predicate = i =>
                                         EF.Functions.ILike(i.ICDCode, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.CompleteDescriptor, $"%{request.SearchTerm}%")
                                         || EF.Functions.ILike(i.ShortDescriptor, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<Diagnosis>(i => i.DiagnosisId);

                return await RetrieveSearchResults<Diagnosis, DiagnosisModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
