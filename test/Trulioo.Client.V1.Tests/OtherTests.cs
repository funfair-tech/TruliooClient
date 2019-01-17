using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trulioo.Client.V1.Model;
using Xunit;
using Xunit.Abstractions;

namespace Trulioo.Client.V1.Tests
{
    public class OtherTests : Basefact
    {
        private readonly ITestOutputHelper _output;
        private string ConfigurationName => "Identity Verification";

        public OtherTests(ITestOutputHelper output)
        {
            _output = output;
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

        [Theory]
        [InlineData("US")]
        [InlineData("CA")]
        [InlineData("NO")]
        [InlineData("NZ")]
        [InlineData("GB")]
        public async Task Should_Get_Consents(string twoDigitIso)
        {
            var truliooClient = GetTruliooClient();
            var consents = await truliooClient.Configuration.GetСonsentsAsync(twoDigitIso, ConfigurationName);
            var countryCodesEnumerated = consents.ToList();
            Assert.NotNull(consents);
            countryCodesEnumerated.ForEach(x => _output.WriteLine(x));
        }

        [Theory]
        [InlineData("US")]
        [InlineData("CA")]
        [InlineData("NO")]
        [InlineData("NZ")]
        [InlineData("GB")]
        public async Task Should_Get_Some_Fields(string twoDigitIso)
        {
            var truliooClient = GetTruliooClient();
            var fields = await truliooClient.Configuration.GetFieldsAsync(twoDigitIso, ConfigurationName);
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
        public async Task Should_Verify_A_Test_Entity()
        {
            var truliooClient = GetTruliooClient();
            var request = GetVerifyRequest1();

            var verifyResult = await truliooClient.Verification.VerifyAsync(request);
            Assert.NotNull(verifyResult);
            Assert.True(verifyResult.Record.RecordStatus == "match");
        }

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
