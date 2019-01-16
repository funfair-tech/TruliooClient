using System.Configuration;
using System.Net;

namespace Trulioo.Client.V1.Tests
{
    public abstract class Basefact
    {
        private const string username = "tbc";
        private const string password = "tbc";
        private const string host = "tbc";

        protected const string IdentityVerificationConfigurationName = "Identity Verification";

        static Basefact()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected TruliooApiClient GetTruliooClient()
        {
            Context context = new Context(username, password);
            if (!string.IsNullOrWhiteSpace(host))
            {
                context.Host = host;
            }

            return new TruliooApiClient(context);
        }
    }
}
