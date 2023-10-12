namespace Identityexercise.ResponseAndRequest.Request
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte age { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
