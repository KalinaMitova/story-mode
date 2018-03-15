﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
  public class InvalidFileNameException : Exception
    {
        private const string ForbiddenSymbolsContainedInName = "The given name contains symbols that" +
                                                               "are not allowed to be used in name of files or folders";
        public InvalidFileNameException()
            :base(ForbiddenSymbolsContainedInName)
        {
        }

        public InvalidFileNameException(string message)
            :base(message)
        {
        }
    }
}