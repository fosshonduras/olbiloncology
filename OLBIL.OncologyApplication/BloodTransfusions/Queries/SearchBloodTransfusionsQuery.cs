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

namespace OLBIL.OncologyApplication.BloodTransfusions.Queries
{
    public sealed class SearchBloodTransfusionsQuery : SearchBase, IRequest<ListModel<BloodTransfusionModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchBloodTransfusionsQuery, ListModel<BloodTransfusionModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BloodTransfusionModel>> Handle(SearchBloodTransfusionsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<BloodTransfusion, bool>> predicate = i => EF.Functions.ILike(i.OncologyPatient.Person.FullName, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<BloodTransfusion>(i => i.BloodTransfusionId);

                return await RetrieveSearchResults<BloodTransfusion, BloodTransfusionModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
