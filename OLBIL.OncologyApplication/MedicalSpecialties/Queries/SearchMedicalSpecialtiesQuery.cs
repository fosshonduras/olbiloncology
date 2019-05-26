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

namespace OLBIL.OncologyApplication.MedicalSpecialties.Queries
{
    public class SearchMedicalSpecialtiesQuery : SearchBase, IRequest<ListModel<MedicalSpecialtyModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchMedicalSpecialtiesQuery, ListModel<MedicalSpecialtyModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<MedicalSpecialtyModel>> Handle(SearchMedicalSpecialtiesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<MedicalSpecialty, bool>> predicate = i => EF.Functions.ILike(i.Description, $"%{request.SearchTerm}%");
                var defaultSort = BuildSortList<MedicalSpecialty>(i => i.MedicalSpecialtyId);

                return await RetrieveSearchResults<MedicalSpecialty, MedicalSpecialtyModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
