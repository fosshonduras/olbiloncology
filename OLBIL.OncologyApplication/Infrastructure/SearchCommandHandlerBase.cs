using AutoMapper;
using MediatR;
using OLBIL.OncologyData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public abstract class SearchCommandHandlerBase<T, R> : IRequestHandler<T, R> where T : IRequest<R>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchCommandHandlerBase(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public abstract Task<R> Handle(T request, CancellationToken cancellationToken);
    }
}
