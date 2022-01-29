using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    internal class PromotionException : ApplicationException
    {
        public PromotionException(string message) : base(message)
        {

        }
    }
}
