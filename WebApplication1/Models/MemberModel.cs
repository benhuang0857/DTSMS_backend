using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace DTSMS.Models
{
    public enum Role
    {
        Admin,
        User,
        Guest
    }

    public enum Status
    {
        Active,
        Inactive,
        Suspended
    }
    public class MemberModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public string Name { get; set; }
        public string MemberNumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public void SetPassword(string password)
        {
            Password = password;
            PasswordHash = HashPassword(password);
        }

        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == PasswordHash;
        }
    }
}
