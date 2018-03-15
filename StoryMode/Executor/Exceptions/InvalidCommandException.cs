using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
    class InvalidCommandException : Exception
    {
        public const string WrongInput = "The command \"{0}\" is invalid!";

        //public InvalidCommandException(string message)
        //    : base(message)
        //{
        //}

        public InvalidCommandException(string input)
           : base(string.Format(WrongInput, input))
        {
        }
    }
}
