using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waRemoteFileSystem.Models
{
    public class RFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
