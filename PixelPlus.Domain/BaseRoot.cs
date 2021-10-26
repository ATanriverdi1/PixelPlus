using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain
{
    public class BaseRoot
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        protected void SetAsCreated()
        {
            var date = DateTime.UtcNow;
            CreatedDate = date;
            ModifiedDate = date;
        }

        protected void SetAsModified()
        {
            var date = DateTime.UtcNow;
            ModifiedDate = date;
            Id = Guid.NewGuid();
        }
    }
}
