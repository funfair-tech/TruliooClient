using Xunit;

namespace Trulioo.Client.V1.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class Connection : Basefact
    {
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
    }
}
