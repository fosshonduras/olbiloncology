﻿using AutoMapper;
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

namespace OLBIL.OncologyApplication.AppointmentReasons.Queries
{
    public class GetAppointmentReasonQuery : IRequest<AppointmentReasonModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetAppointmentReasonQuery, AppointmentReasonModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<AppointmentReasonModel> Handle(GetAppointmentReasonQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<AppointmentReasonModel>(await Context
                    .AppointmentReasons.Where(o => o.AppointmentReasonId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(AppointmentReason), nameof(item.AppointmentReasonId), request.Id);
                }

                return item;
            }
        }
    }
}