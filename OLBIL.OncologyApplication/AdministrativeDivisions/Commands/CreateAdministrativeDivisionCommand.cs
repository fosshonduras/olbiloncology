using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Commands
{
    public class CreateAdministrativeDivisionCommand : IRequest<int>
    {
        public AdministrativeDivisionModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateAdministrativeDivisionCommand, int>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateAdministrativeDivisionCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var ward = await Context.AdministrativeDivisions
                    .Where(p => p.AdministrativeDivisionId == model.AdministrativeDivisionId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (ward != null)
                {
                    throw new AlreadyExistsException(nameof(AdministrativeDivision), nameof(model.AdministrativeDivisionId), model.AdministrativeDivisionId);
                }

                var newRecord = new AdministrativeDivision
                {
                    Name = model.Name,
                    Code = model.Code
                };

                Context.AdministrativeDivisions.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.AdministrativeDivisionId;
            }
        }
    }
}
