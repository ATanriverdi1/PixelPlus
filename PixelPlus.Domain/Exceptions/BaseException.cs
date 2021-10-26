using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public string Key { get; set; }
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();

        public BaseException()
        {

        }

        public BaseException(string key, params KeyValuePair<string, string>[] param)
        {
            Key = key;

            if (param != null)
            {
                Params = param.ToDictionary(p => p.Key, p => p.Value);
            }
        }
    }
}