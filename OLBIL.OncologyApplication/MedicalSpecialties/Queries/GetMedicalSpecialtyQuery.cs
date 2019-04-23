using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Queries
{
    public class GetMedicalSpecialtyQuery : IRequest<MedicalSpecialtyModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetMedicalSpecialtyQuery, MedicalSpecialtyModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<MedicalSpecialtyModel> Handle(GetMedicalSpecialtyQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<MedicalSpecialtyModel>(await Context
                    .MedicalSpecialties.Where(o => o.MedicalSpecialtyId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(MedicalSpecialty), nameof(item.MedicalSpecialtyId), request.Id);
                }

                return item;
            }
        }
    }
}