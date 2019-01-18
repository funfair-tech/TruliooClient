using System.Linq;
using System.Threading.Tasks;
using Trulioo.Client.V1.Model;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Trulioo.Client.V1.Tests
{
    /// <summary>
    /// Integration tests against the Trulioo API using demo credentials.
    /// Verifies the basics against each public endpoint.
    /// </summary>
    public class TruliooApiIntegrationTests : Basefact
    {
        private readonly ITestOutputHelper _output;

        public TruliooApiIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async void SayHello_Success()
        {
            //Arrange
            using (var service = GetTruliooClient())
            {
                var userName = "testuser";

                //Act
                var response = await service.Connection.SayHelloAsync(userName);

                //Assert
                Assert.NotNull(response);
                Assert.Contains("testuser", response);
            }
        }

        [Fact]
        public async void Authentication_Success()
        {
            //Arrange
            using (var service = GetTruliooClient())
            {
                //Act
                var response = await service.Connection.TestAuthenticationAsync();

                //Assert
                Assert.NotNull(response);
            }
        }

        [Fact]
        public async Task Should_Get_Country_Codes()
        {
            var truliooClient = GetTruliooClient();
            var countryCodes = await truliooClient.Configuration.GetCountryCodesAsync(ConfigurationName);
            var countryCodesEnumerated = countryCodes.ToList();
            Assert.NotNull(countryCodes);
            countryCodesEnumerated.ForEach(x => _output.WriteLine(x));
        }

        /// <remarks>
        /// Inline data items should match the values returned from GetCountryCodesAsync(ConfigurationName)
        /// </remarks>
        [Theory]
        [InlineData("US")]
        [InlineData("CA")]
        [InlineData("NO")]
        [InlineData("NZ")]
        [InlineData("GB")]
        public async Task Should_Get_Consents(string twoCharacterCountryIsoCode)
        {
            var truliooClient = GetTruliooClient();
            var consents = await truliooClient.Configuration.GetСonsentsAsync(twoCharacterCountryIsoCode, ConfigurationName);
            var countryCodesEnumerated = consents.ToList();
            Assert.NotNull(consents);
            countryCodesEnumerated.ForEach(x => _output.WriteLine(x));
        }

        /// <remarks>
        /// Inline data items should match the values returned from GetCountryCodesAsync(ConfigurationName)
        /// </remarks>
        [Theory]
        [InlineData("US")]
        [InlineData("CA")]
        [InlineData("NO")]
        [InlineData("NZ")]
        [InlineData("GB")]
        public async Task Should_Get_Fields_For_Given_Territory(string twoCharacterCountryIsoCode)
        {
            var truliooClient = GetTruliooClient();
            var fields = await truliooClient.Configuration.GetFieldsAsync(twoCharacterCountryIsoCode, ConfigurationName);
            _output.WriteLine($"Fields: {fields}");
            var fieldsEnumerated = fields.ToList();
            Assert.NotNull(fields);
            fieldsEnumerated.ForEach(x =>
            {
                Assert.NotNull(x.Key);
                Assert.NotNull(x.Value);
                _output.WriteLine($"Key: {x.Key}, Value: {x.Value}");
            });
        }

        [Fact]
        public async Task Should_Verify_A_Test_Entity_With_Identity_Match()
        {
            var truliooClient = GetTruliooClient();
            var request = GetVerifyRequest1();

            var verifyResult = await truliooClient.Verification.VerifyAsync(request);
            Assert.NotNull(verifyResult);
            Assert.True(verifyResult.Record.RecordStatus == "match");
        }

        /// <remarks>
        /// one (of several) test entities that are available in demo mode.
        /// data comes from the api call to testentities - see https://developer.trulioo.com/reference#get-test-entities
        /// </remarks>
        private static VerifyRequest GetVerifyRequest1()
        {
            var request = new VerifyRequest
            {
                AcceptTruliooTermsAndConditions = true,
                Demo = true,
                CleansedAddress = false,
                ConfigurationName = "Identity Verification",
                CountryCode = "US",
                DataFields = new DataFields
                {
                    PersonInfo = new PersonInfo
                    {
                        FirstGivenName = "Smith",
                        MiddleName = "M",
                        FirstSurName = "James",
                        DayOfBirth = 3,
                        MonthOfBirth = 4,
                        YearOfBirth = 1982,
                        Gender = "M"
                    },
                    Location = new Location
                    {
                        BuildingNumber = "452",
                        UnitNumber = "2",
                        StreetName = "Michigan",
                        StreetType = "Avenue",
                        City = "Chicago",
                        StateProvinceCode = "MI",
                        PostalCode = "90010"
                    },
                    Communication = new Communication
                    {
                        Telephone = "221-214-4456"
                    },
                    DriverLicence = new DriverLicence
                    {
                        Number = "L2345671",
                        State = "CA"
                    },
                    NationalIds = new[]
                    {
                        new NationalId
                        {
                            Number = "929-02-1234",
                            Type = "socialservice"
                        }
                    },
                    CountrySpecific = new CountrySpecific
                    {
                        {
                            "US",
                            new Dictionary<string, string>
                            {
                                {"WatchlistState", "Clear"},
                                {"FraudFlag", "true"},
                                {"IsDeceased", "false"}
                            }
                        }
                    }
                }
            };
            return request;
        }
    }
}
