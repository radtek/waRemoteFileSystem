using System.Collections.Generic;

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
