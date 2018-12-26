using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.Exceptions
{
   public class UserNotExist : Exception
    {
        public UserNotExist() { }
        public UserNotExist(string message) : base(message) { }
        public UserNotExist(string message, Exception inner) : base(message, inner) { }


    }
}
