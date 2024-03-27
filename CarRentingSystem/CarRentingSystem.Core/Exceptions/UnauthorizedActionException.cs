using System;
namespace CarRentingSystem.Core.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException() { }

        public UnauthorizedActionException(string message)
            : base(message) { }
    }
}

