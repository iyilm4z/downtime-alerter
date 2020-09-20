using System.ComponentModel.DataAnnotations;
using DowntimeAlerter.Web.Mvc.Models;

namespace DowntimeAlerter.Web.Models.Authentication
{
    public class LoginModel : AppModelBase
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
