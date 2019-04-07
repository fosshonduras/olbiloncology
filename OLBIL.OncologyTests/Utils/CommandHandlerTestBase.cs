using OLBIL.OncologyData;
using System;

namespace OLBIL.OncologyTests.Utils
{
    public class CommandHandlerTestBase : IDisposable
    {
        protected readonly OncologyContext _context;

        public CommandHandlerTestBase()
        {
            _context = OncologyContextFactory.Create();
        }

        public void Dispose()
        {
            OncologyContextFactory.Destroy(_context);
        }
    }
}
