using System;

namespace OLBIL.OncologyApplication.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string keyName, object key)
            : base($"Entity \"{name}\" with {keyName} ({key}) was not found.")
        {
        }
    }
}