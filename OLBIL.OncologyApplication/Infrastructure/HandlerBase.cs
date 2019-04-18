using AutoMapper;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class HandlerBase
    {
        public HandlerBase(IOncologyContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public IOncologyContext Context { get; }
        public IMapper Mapper { get; }
    }
}
