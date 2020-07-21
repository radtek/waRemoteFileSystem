using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waRemoteFileSystem.Models
{
    public class RList
    {
        public List< RFile> Files { get; set; }
        public List< RDirectory > Directories { get; set; }

        public RList()
        {
            Files = new List<RFile>();
            Directories = new List<RDirectory>();
        }
    }
}
