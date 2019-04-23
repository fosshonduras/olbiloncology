using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Commands
{
    public class UpdateMedicalSpecialtyCommand: IRequest
    {
        public MedicalSpecialtyModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateMedicalSpecialtyCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }
            public async Task<Unit> Handle(UpdateMedicalSpecialtyCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.MedicalSpecialties
                    .Where(p => p.MedicalSpecialtyId == model.MedicalSpecialtyId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(MedicalSpecialty), nameof(model.MedicalSpecialtyId), model.MedicalSpecialtyId);
                }

                item.Description = model.Description;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
