using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Triedy
{
    public class PocetUuid
    {
        public Guid guid { get; set; }
        public int pocet { get; set; }

        public PocetUuid(Guid paGuid)
        {
            guid = paGuid;
            pocet = 0;
        }

    }
}
