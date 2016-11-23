namespace ContinuousIntegration.IntegrationTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Xunit;

    public class ValuesControllerIntegrationTests
    {
        #region Fields

        private const string baseUrl = "http://localhost";
        private const string resourceUrl = "/api/values";

        #endregion Fields

        #region Test Methods

        [Fact]
        public async Task Can_Get_Default_Values()
        {
            // Arrange
            var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            var finalResourceUrl = resourceUrl;

            // Act
            var requestResult = await httpClient.GetAsync(finalResourceUrl);
            var content = await requestResult.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(requestResult.StatusCode, HttpStatusCode.OK);
            Assert.Equal(@"[""value1"",""value2""]", content);
        }

        [Fact(Skip = "Used to test ci builds to perform a controlled failure")]
        public void Will_Cause_A_Failure()
        {
            Assert.Equal(false, true);
        }

        #endregion Test Methods
    }
}
