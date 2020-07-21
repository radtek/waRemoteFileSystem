using System.ComponentModel.DataAnnotations;

namespace waRemoteFileSystem.DataBase
{
    public class spRole
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string UserAccess { get; set; }
    }
}
