using System.Configuration;

namespace PortalGRFP.Models.Gmail
{
    public class GoogleConstant
    {
        public static string ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        public static string ClientId = ConfigurationManager.AppSettings["ClientId"];
        public static string RedirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];
        public static string Oauth2Url = "https://accounts.google.com/o/oauth2/auth?";
        public static string Scopes = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
    }
}