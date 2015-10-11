using System.Configuration;

namespace RecruitmentManagementSystem.App
{
    public class OAuthConfig
    {
        public static string FacebookAppId
        {
            get { return ConfigurationManager.AppSettings["FacebookAppId"]; }
        }

        public static string FacebookAppSecret
        {
            get { return ConfigurationManager.AppSettings["FacebookAppSecret"]; }
        }

        public static string GoogleClientId
        {
            get { return ConfigurationManager.AppSettings["GoogleClientId"]; }
        }

        public static string GoogleClientSecret
        {
            get { return ConfigurationManager.AppSettings["GoogleClientSecret"]; }
        }
    }
}