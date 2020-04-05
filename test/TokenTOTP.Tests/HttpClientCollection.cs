using Xunit;

namespace TokenTOTP.Tests
{
    [CollectionDefinition(Name)]
    public class HttpClientCollection : ICollectionFixture<HttpClientFixture>
    {
        public const string Name = "Http Collection";
    }
}