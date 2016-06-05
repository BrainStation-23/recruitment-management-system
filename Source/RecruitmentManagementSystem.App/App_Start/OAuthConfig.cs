using System.Configuration;

namespace RecruitmentManagementSystem.App
{
    public class OAuthConfig
    {
        public static string FacebookAppId => ConfigurationManager.AppSettings["FacebookAppId"];

        public static string FacebookAppSecret => ConfigurationManager.AppSettings["FacebookAppSecret"];

        public static string GoogleClientId => ConfigurationManager.AppSettings["GoogleClientId"];

        public static string GoogleClientSecret => ConfigurationManager.AppSettings["GoogleClientSecret"];
    }
}
