using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trulioo.Client.V1.Model;
using Trulioo.Client.V1.URI;

namespace Trulioo.Client.V1
{
    /// <summary>
    ///     Provides a class for working with Trulioo Configuration.
    /// </summary>
    public class Configuration : IConfiguration
    {
        private readonly Namespace _configurationNamespace = new Namespace(value: "configuration");

        private readonly TruliooApiClient _service;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        /// <param name="service">
        ///     An object representing the root of Trulioo configuration service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="service" /> is <c>null</c>.
        /// </exception>
        protected internal Configuration(TruliooApiClient service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private Context Context => this._service?.Context;

        /// <summary>
        ///     Consents required for the provided country and configuration
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="configurationName"></param>
        public async Task<IEnumerable<string>> GetСonsentsAsync(string countryCode, string configurationName)
        {
            ResourceName resource = new ResourceName("consents", configurationName, countryCode);
            IEnumerable<string> response = await this.Context.GetAsync<IEnumerable<string>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Get Country Codes configured for your account
        /// </summary>
        /// <param name="configurationName"></param>
        public async Task<IEnumerable<string>> GetCountryCodesAsync(string configurationName)
        {
            ResourceName resource = new ResourceName("countrycodes", configurationName);
            IEnumerable<string> response = await this.Context.GetAsync<IEnumerable<string>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Gets the provinces states or other subdivisions for a country, mostly matches ISO 3166-2
        /// </summary>
        /// <param name="countryCode"></param>
        public async Task<IEnumerable<CountrySubdivision>> GetCountrySubdivisionsAsync(string countryCode)
        {
            ResourceName resource = new ResourceName("countrysubdivisions", countryCode);
            IList<CountrySubdivision> response = await this.Context.GetAsync<IList<CountrySubdivision>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Generates json schema for the API, the schema is dynamic based on the country and configuration you are using.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="configurationName"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, dynamic>> GetFieldsAsync(string countryCode, string configurationName)
        {
            ResourceName resource = new ResourceName("fields", configurationName, countryCode);
            Dictionary<string, dynamic> response = await this.Context.GetAsync<Dictionary<string, dynamic>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Gets a list of TestEntities as a list of DataFields objects
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="configurationName"></param>
        /// <returns>List of Datafields object</returns>
        public async Task<IEnumerable<DataFields>> GetTestEntitiesAsync(string countryCode, string configurationName)
        {
            ResourceName resource = new ResourceName("testentities", configurationName, countryCode);
            IList<DataFields> response = await this.Context.GetAsync<IList<DataFields>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Gets a list of Datasources
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="configurationName"></param>
        /// <returns> List of Datsource Group Countries </returns>
        public async Task<IEnumerable<NormalizedDatasourceGroupCountry>> GetDatasourcesAsync(string countryCode, string configurationName)
        {
            ResourceName resource = new ResourceName("datasources", configurationName, countryCode);
            IList<NormalizedDatasourceGroupCountry> response = await this.Context.GetAsync<IList<NormalizedDatasourceGroupCountry>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Gets a list of document types
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns> Dictionary with Keys as the Country Codes and Values as list of document types for that country </returns>
        public async Task<Dictionary<string, IList<string>>> GetDocumentTypesAsync(string countryCode = null)
        {
            ResourceName resource = countryCode != null ? new ResourceName("documentTypes", countryCode) : new ResourceName("documentTypes");

            Dictionary<string, IList<string>> response = await this.Context.GetAsync<Dictionary<string, IList<string>>>(this._configurationNamespace, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }
    }

    public interface IConfiguration
    {
        Task<Dictionary<string, dynamic>> GetFieldsAsync(string countryCode, string configurationName);

        Task<IEnumerable<string>> GetСonsentsAsync(string countryCode, string configurationName);

        Task<IEnumerable<string>> GetCountryCodesAsync(string configurationName);
    }
}