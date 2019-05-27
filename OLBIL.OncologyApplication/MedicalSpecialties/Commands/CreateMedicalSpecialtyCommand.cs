using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Commands
{
    public class CreateMedicalSpecialtyCommand : IRequest<int>
    {
        public MedicalSpecialtyModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateMedicalSpecialtyCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateMedicalSpecialtyCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.MedicalSpecialties
                    .Where(p => p.MedicalSpecialtyId == model.MedicalSpecialtyId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(MedicalSpecialty), nameof(model.MedicalSpecialtyId), model.MedicalSpecialtyId);
                }

                var newRecord = new MedicalSpecialty
                {
                    Description = model.Description
                };

                Context.MedicalSpecialties.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.MedicalSpecialtyId;
            }
        }
    }
}