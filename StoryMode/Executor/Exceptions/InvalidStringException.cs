using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
   public class InvalidStringException : Exception
    {
        private const string NullOrEmptyString = "NullOrEmptyValue";

        public InvalidStringException()
            :base(NullOrEmptyString)
        {
        }

        public InvalidStringException(string message)
            :base(message)
        {
        }
    }
}
