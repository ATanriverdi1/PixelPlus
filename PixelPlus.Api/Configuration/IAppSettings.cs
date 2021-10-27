using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Configuration
{
    public interface IAppSettings
    {
        public string Secret { get; set; }
    }
}
