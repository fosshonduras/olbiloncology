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

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Commands
{
    public class UpdateAdministrativeDivisionCommand : IRequest
    {
        public AdministrativeDivisionModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateAdministrativeDivisionCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateAdministrativeDivisionCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.AdministrativeDivisions
                    .Where(p => p.AdministrativeDivisionId == model.AdministrativeDivisionId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(AdministrativeDivision), nameof(model.AdministrativeDivisionId), model.AdministrativeDivisionId);
                }

                item.ParentId = model.ParentId;
                item.Code = model.Code;
                item.Name = model.Name;
                item.Level = model.Level;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
