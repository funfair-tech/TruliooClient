using System.Configuration;
using System.Net;
using System.Net.Http;

namespace Trulioo.Client.V1.Tests
{
    public abstract class Basefact
    {
        private const string username = "FunFairTechnologies_Demo_API";
        private const string password = "jgJ@!4PAig4sST3fUT5u";
        private const string host = "api.globaldatacompany.com";

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
            Context context = new Context(username, password, new HttpClient());
            if (!string.IsNullOrWhiteSpace(host))
            {
                context.Host = host;
            }

            return new TruliooApiClient(context);
        }
    }
}
