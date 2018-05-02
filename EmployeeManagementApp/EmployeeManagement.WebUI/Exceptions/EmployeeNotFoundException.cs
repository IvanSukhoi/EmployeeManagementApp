using System;
using System.Runtime.Serialization;

namespace EmployeeManagement.WebUI.Exceptions
{
    [Serializable()]
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException()
        {}

        public EmployeeNotFoundException(string message) : base(message)
        {}

        public EmployeeNotFoundException(string message, Exception inner) : base(message, inner)
        {}

        protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {}
    }
}