namespace Auth.Server.Models.Authorization
{
    public static class AuthorizationConfiguration
    {
        public static bool RequireConfirmedEmail = false;
        public static int RequiredLength = 6;
        public static bool RequireNonAlphanumeric = false;
        public static bool RequireUppercase = false;
        public static bool RequireLowercase = false;
        public static string HostUrl = "http://localhost:8046/api/Account/ConfirmEmail";
    }
}
