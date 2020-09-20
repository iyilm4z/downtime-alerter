using System;
using Microsoft.AspNetCore.Http;

namespace DowntimeAlerter
{
    public class AppDefaults
    {
        public const string AppName = "DowntimeAlerter";

        public const string AdminAreaName = "Admin";

        public const string Version = "1.00";

        private static readonly Lazy<DateTime> LazyReleaseDate = new Lazy<DateTime>(() => DateTime.Now);
        public static DateTime ReleaseDate => LazyReleaseDate.Value;

        public const string AdminUserName = "admin";

        public class Cookie
        {
            public const string Prefix = ".DA";

            public const string User = ".User";

            public const string Authentication = ".Authentication";

            public const string Antiforgery = ".Antiforgery";
        }

        public class Authentication
        {
            public const string ClaimsIssuer = AppName;

            public const string AuthenticationScheme = "Authentication";

            public static PathString LoginPath => new PathString("/Home/Login");
        }
    }
}