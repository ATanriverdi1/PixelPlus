using PixelPlus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : this("NOT_FOUND")
        {
        }

        public NotFoundException(string key, params KeyValuePair<string, string>[] param) : base(key, param)
        {
        }
    }
}
