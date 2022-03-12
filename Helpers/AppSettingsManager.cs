
namespace GenericApi.Helpers
{
    public static class AppSettingsManager
    {
        public static string GetSecretAuthKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get("SecretAuthKey");
        }

        public static string GetSecretMySqlPasswDecodeKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get("SecretMySqlPasswDecodeKey");
        }
    }
}
