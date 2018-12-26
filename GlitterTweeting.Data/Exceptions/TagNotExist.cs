using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.Exceptions
{
    public   class TagNotExist : Exception
    {
        public TagNotExist() { }
        public TagNotExist(string message) : base(message) { }
        public TagNotExist(string message, Exception inner) : base(message, inner) { }


    }
    
}
