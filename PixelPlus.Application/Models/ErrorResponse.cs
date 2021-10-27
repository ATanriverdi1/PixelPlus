using PixelPlus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Application.Models
{
    public class ErrorResponse
    {
        public string Key { get; set; }
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();


        public ErrorResponse(BaseException ex)
        {
            Key = ex.Key;
            Params = ex.Params;
        }

        public ErrorResponse(string key, params KeyValuePair<string, string>[] param)
        {
            Key = key;

            if (param != null)
            {
                Params = param.ToDictionary(p => p.Key, p => p.Value);
            }
        }
    }

}
