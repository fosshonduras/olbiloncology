using System;

namespace OLBIL.OncologyApplication.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, string keyName, object key)
            : base($"Entity \"{name}\" with {keyName} ({key}) already exists.")
        {
        }
    }
}