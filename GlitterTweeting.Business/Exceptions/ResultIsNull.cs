using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Exceptions
{
   public class ResultIsNull : Exception
    {
        public ResultIsNull()
        {

        }
        public ResultIsNull(string message) : base(message) { }
        public ResultIsNull(string message, Exception inner) : base(message, inner) { }

    }
}
