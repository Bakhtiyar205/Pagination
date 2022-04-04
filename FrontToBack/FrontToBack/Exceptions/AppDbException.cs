using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Exceptions
{
    public class AppDbException : Exception
    {
        public AppDbException(string message) : base(message)
        {
        }
    }
}
