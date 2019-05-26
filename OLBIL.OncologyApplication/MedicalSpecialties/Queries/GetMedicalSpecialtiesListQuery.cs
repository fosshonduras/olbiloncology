using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Queries
{
    public class GetMedicalSpecialtiesListQuery: GetListBase, IRequest<ListModel<MedicalSpecialtyModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetMedicalSpecialtiesListQuery, ListModel<MedicalSpecialtyModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<MedicalSpecialtyModel>> Handle(GetMedicalSpecialtiesListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<MedicalSpecialty>(i => i.MedicalSpecialtyId);

                return await RetrieveListResults<MedicalSpecialty,MedicalSpecialtyModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
