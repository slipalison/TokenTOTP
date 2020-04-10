using OtpNet;

namespace TokenTOTP.Domain.Services
{
    public class OtpNetService
    {
        private const int DefaultTimeRange = 30;
        private const int DefaultTokenSize = 6;

        // Use Sha1 so it can work with Google Authenticator
        private const OtpHashMode HASH_MODE = OtpHashMode.Sha1;

        public (string token, long timeToLive) GenerateToken(byte[] seed, int timeRange = DefaultTimeRange, int tokenSize = DefaultTokenSize)
        {
            var generator = GetTotp(seed, timeRange, tokenSize);
            return (generator.ComputeTotp(), generator.RemainingSeconds());
        }

        public static (string seed, byte[] seedBytes) CreateSeed()
        {
            // must be based in a HashMode or it may not work with all client libraries. Specially JS libs
            var bytes = KeyGeneration.GenerateRandomKey(HASH_MODE);
            return (Base32Encoding.ToString(bytes), bytes);
        }

        private Totp GetTotp(byte[] seed, int timeRange, int tokenSize)
        {
            return new Totp(seed, timeRange, HASH_MODE, tokenSize);
        }
    }
}
