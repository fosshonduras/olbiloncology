using AutoMapper;
using OLBIL.OncologyData;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class HandlerBase
    {
        public HandlerBase(OncologyContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public OncologyContext Context { get; }
        public IMapper Mapper { get; }
    }
}
