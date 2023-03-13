using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderDomainException : Exception
    {
        public OrderDomainException()
        {
            
        }

        public OrderDomainException(string message):base(message)
        {
            
        }

        public OrderDomainException(string message, Exception innerException):base(message, innerException)
        {
            
        }
    }
}
