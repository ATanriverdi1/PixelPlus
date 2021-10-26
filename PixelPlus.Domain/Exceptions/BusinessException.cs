using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException() { }

        public BusinessException(string key, params KeyValuePair<string, string>[] param) : base(key, param)
        {
        }
    }
}
