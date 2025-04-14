namespace BirdWare.Domain.Models
{
    public class Bruger
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
