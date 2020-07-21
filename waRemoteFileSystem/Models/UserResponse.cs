namespace waRemoteFileSystem.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Access { get; set; }
        public string Token { get; set; }

        public int Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
