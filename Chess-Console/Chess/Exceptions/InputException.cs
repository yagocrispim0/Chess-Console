using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Exceptions
{
    internal class InputException : ApplicationException
    {
        public InputException(String message) : base(message)
        {

        }
    }
}
