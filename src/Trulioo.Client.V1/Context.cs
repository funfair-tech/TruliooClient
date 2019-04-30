using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Trulioo.Client.V1.Exceptions;
using Trulioo.Client.V1.Model.Errors;
using Trulioo.Client.V1.URI;
using UnauthorizedAccessException = Trulioo.Client.V1.Exceptions.UnauthorizedAccessException;

namespace Trulioo.Client.V1
{
    /// <summary>
    ///     Provides a class for sending HTTP requests and receiving HTTP responses from a Trulioo server.
    /// </summary>
    public class Context
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
                                                                                {
                                                                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc, DateFormatHandling = DateFormatHandling.IsoDateFormat
                                                                                };

        private readonly string _credentials;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _httpClientName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Context" /> class with a host and optional message handler, protocol by default is HTTPS.
        /// </summary>
        /// <param name="userName">User name for current context</param>
        /// <param name="password">Password for current context</param>
        /// <param name="client">The fully configured http client, from the application context.</param>
        /// <paramref name="userName" />
        /// is
        /// <c>null</c>
        /// or empty.
        /// <paramref name="password" />
        /// is
        /// <c>null</c>
        /// or empty.
        public Context(string userName, string password, IHttpClientFactory httpClientFactory, string httpClientName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrEmpty(httpClientName))
            {
                throw new ArgumentNullException(nameof(httpClientName));
            }

            this._httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this._httpClientName = httpClientName;

            this._credentials = EncodeCredentials(userName, password);
        }

        /// <summary>
        ///     Gets the Trulioo host name associated with the current <see cref="Context" />.
        /// </summary>
        /// <value>
        ///     A Trulioo host name.
        /// </value>
        public string Host { get; set; } = "api.globaldatacompany.com";

        private HttpClient HttpClient
        {
            get
            {
                HttpClient httpClient = this._httpClientFactory.CreateClient(this._httpClientName);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                httpClient.DefaultRequestHeaders.Add(name: "User-Agent", value: "trulioo-sdk-csharp/1.1");
                return httpClient;
            }
        }

        /// <summary>
        ///     Sends a GET request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <returns>
        ///     The response to the GET request.
        /// </returns>
        internal async Task<TReturn> GetAsync<TReturn>(Namespace ns, ResourceName resource)
        {
            TReturn response = await this.SendAsync<TReturn>(HttpMethod.Get, ns, resource)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        /// <summary>
        ///     Sends a POST request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <param name="content">
        ///     An object identifying the HTTP content.
        /// </param>
        /// <returns>
        ///     The response to the POST request.
        /// </returns>
        internal async Task PostAsync(Namespace ns, ResourceName resource, dynamic content = null)
        {
            await SendAsync(HttpMethod.Post, ns, resource, content)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        ///     Sends a POST request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <param name="content">
        ///     An object identifying the HTTP content.
        /// </param>
        /// <returns>
        ///     The response to the POST request.
        /// </returns>
        internal async Task<TReturn> PostAsync<TReturn>(Namespace ns, ResourceName resource, dynamic content = null)
        {
            dynamic response = await SendAsync<TReturn>(HttpMethod.Post, ns, resource, content)
                .ConfigureAwait(false);

            return response;
        }

        /// <summary>
        ///     Sends a PUT request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <param name="content">
        ///     An object identifying the HTTP content.
        /// </param>
        /// <returns>
        ///     The response to the PUT request.
        /// </returns>
        internal async Task<TReturn> PutAsync<TReturn>(Namespace ns, ResourceName resource, dynamic content = null)
        {
            dynamic response = await SendAsync<TReturn>(HttpMethod.Put, ns, resource, content)
                .ConfigureAwait(false);

            return response;
        }

        /// <summary>
        ///     Sends a PUT request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <param name="content">
        ///     An object identifying the HTTP content.
        /// </param>
        /// <returns>
        ///     The response to the PUT request.
        /// </returns>
        internal async Task PutAsync(Namespace ns, ResourceName resource, dynamic content = null)
        {
            await SendAsync(HttpMethod.Put, ns, resource, content)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        ///     Sends a DELETE request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        ///     An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        ///     An object identifying a resource.
        /// </param>
        /// <param name="content">
        ///     An object identifying the HTTP content.
        /// </param>
        /// <returns>
        ///     The response to the DELETE request.
        /// </returns>
        internal async Task DeleteAsync(Namespace ns, ResourceName resource, dynamic content = null)
        {
            await SendAsync(HttpMethod.Delete, ns, resource, content)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        private Uri CreateServiceUri(Namespace ns, ResourceName name)
        {
            StringBuilder builder = new StringBuilder(value: "https://");
            builder.Append(this.Host);
            builder.Append(ns.ToUriString());
            builder.Append(value: "/v1/");
            builder.Append(name.ToUriString());

            Uri uri = new Uri(builder.ToString(), UriKind.Absolute);

            return uri;
        }

        private static StringContent GetStringContent(dynamic content)
        {
            if (object.ReferenceEquals(content, objB: null))
            {
                return null;
            }

            return new StringContent(JsonConvert.SerializeObject(content, JsonSerializerSettings), Encoding.UTF8, mediaType: "application/json");
        }

        private async Task<TReturn> SendAsync<TReturn>(HttpMethod httpMethod, Namespace ns, ResourceName resource, dynamic content = null)
        {
            dynamic response = await SendInternalAsync(httpMethod, ns, resource, content)
                .ConfigureAwait(false);

            dynamic message = typeof(TReturn) == typeof(string)
                ? await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false)
                : JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync()
                                                             .ConfigureAwait(false));

            return message;
        }

        private async Task<HttpResponseMessage> SendInternalAsync(HttpMethod httpMethod, Namespace ns, ResourceName resource, dynamic content = null)
        {
            Uri serviceUri = this.CreateServiceUri(ns, resource);
            dynamic stringContent = GetStringContent(content);

            using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, serviceUri) {Content = stringContent})
            {
                request.Headers.Add(name: "Authorization", $"Basic {this._credentials}");

                HttpResponseMessage response = await this.HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None)
                    .ConfigureAwait(continueOnCapturedContext: false);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowRequestExceptionAsync(response)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }

                return response;
            }
        }

        /// <summary>
        ///     Throw request exception asynchronous.
        /// </summary>
        /// <exception cref="RequestException">
        ///     Thrown when a Request error condition occurs.
        /// </exception>
        /// <returns>
        ///     A <see cref="Task" /> representing the operation.
        /// </returns>
        private static async Task ThrowRequestExceptionAsync(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
            Error error = ParseError(response.StatusCode, content);

            RequestException requestException;

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    requestException = new BadRequestException(error.Message, error.Code, error.Reason);

                    break;
                case HttpStatusCode.Forbidden:
                    requestException = new UnauthorizedAccessException(error.Message, error.Code, error.Reason);

                    break;
                case HttpStatusCode.InternalServerError:
                    requestException = new InternalServerErrorException(error.Message, error.Code, error.Reason);

                    break;
                case HttpStatusCode.NotFound:
                    requestException = new ResourceNotFoundException(error.Message, error.Code, error.Reason);

                    break;
                case HttpStatusCode.Unauthorized:
                    requestException = new AuthenticationFailureException(error.Message, error.Code, error.Reason);

                    break;
                default:
                    requestException = new RequestException(error.Message, error.Code, error.Reason);

                    break;
            }

            throw requestException;
        }

        private static Error ParseError(HttpStatusCode statusCode, string content)
        {
            Error error;

            try
            {
                error = JsonConvert.DeserializeObject<Error>(content) ?? new Error {Code = (int) statusCode, Message = string.IsNullOrEmpty(content) ? statusCode.ToString() : content};
            }
            catch (Exception ex)
            {
                error = new Error {Code = (int) statusCode, Message = string.IsNullOrEmpty(content) ? statusCode.ToString() : content, Reason = ex.Message};
            }

            return error;
        }

        private static string EncodeCredentials(string userName, string password)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes($"{userName}:{password}");

            return Convert.ToBase64String(byteArray);
        }
    }
}