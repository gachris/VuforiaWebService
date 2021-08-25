namespace VuforiaWebService.Api.Core.Types
{
    public class DatabaseAccessKeys
    {
        public DatabaseAccessKeys(string accessKey, string secretKey)
        {
            this.AccessKey = accessKey;
            this.SecretKey = secretKey;
        }

        public string AccessKey { get; }
        public string SecretKey { get; }
    }
}
