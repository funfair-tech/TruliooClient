using System.Configuration;
using System.Net;
using System.Net.Http;

namespace Trulioo.Client.V1.Tests
{
    public abstract class Basefact
    {
        // demo properties.
        private const string Username = "FunFairTechnologies_Demo_API";
        private const string Password = "jgJ@!4PAig4sST3fUT5u";
        private const string Host = "api.globaldatacompany.com";

        protected const string ConfigurationName = "Identity Verification";

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
            Context context = new Context(Username, Password, new HttpClient());
            if (!string.IsNullOrWhiteSpace(Host))
            {
                context.Host = Host;
            }

            return new TruliooApiClient(context);
        }
    }
}
