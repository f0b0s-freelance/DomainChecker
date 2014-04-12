using System;

namespace DomainChecker.Core
{
    public class DomainCheckerException : Exception
    {
        public DomainCheckerException() : base()
        {
            
        }

        public DomainCheckerException(string message) : base(message)
        {
            
        }

        public DomainCheckerException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
