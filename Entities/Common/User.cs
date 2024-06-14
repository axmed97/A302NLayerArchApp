using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    public class User : AppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OTP { get; set; }
        public DateTime? ExpiredDate { get; set; }
        [NotMapped]
        public override string? PhotoUrl { get => base.PhotoUrl; set => base.PhotoUrl = value; }
    }
}
