using System;

namespace OLBIL.OncologyApplication.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" with key ({key}) already exists.")
        {
        }
    }
}