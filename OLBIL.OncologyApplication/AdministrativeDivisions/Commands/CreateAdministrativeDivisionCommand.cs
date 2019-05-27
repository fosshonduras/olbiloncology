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
    public class CreateAdministrativeDivisionCommand : CreateBase<AdministrativeDivisionModel>, IRequest<int>
    {
        public class Handler : HandlerBase, IRequestHandler<CreateAdministrativeDivisionCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateAdministrativeDivisionCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.AdministrativeDivisions
                    .Where(p => p.AdministrativeDivisionId == model.AdministrativeDivisionId)
                    .FirstOrDefaultAsync(cancellationToken);

                ThrowAlreadyExistsExceptionIfNull(model, item);

                var newRecord = new AdministrativeDivision
                {
                    ParentId = model.ParentId,
                    Name = model.Name,
                    Code = model.Code,
                    Level = model.Level
                };

                Context.AdministrativeDivisions.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.AdministrativeDivisionId;
            }

            protected void ThrowAlreadyExistsExceptionIfNull(AdministrativeDivisionModel model, AdministrativeDivision entity)
            {
                if (entity != null)
                {
                    throw new AlreadyExistsException(
                        entity.GetType().Name, 
                        nameof(model.AdministrativeDivisionId), 
                        model.AdministrativeDivisionId
                    );
                }
            }
        }
    }
}
