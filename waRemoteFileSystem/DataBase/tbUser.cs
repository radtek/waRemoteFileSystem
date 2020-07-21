using System.ComponentModel.DataAnnotations;

namespace waRemoteFileSystem.DataBase
{
    public class tbUser
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }


        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        public string EMail { get; set; }

        [Required]
        public int RoleId { get; set; }
        public spRole Role { get; set; }
    }
}
