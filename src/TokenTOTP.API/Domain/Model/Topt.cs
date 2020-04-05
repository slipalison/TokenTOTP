using System;

namespace TokenTOTP.API.Domain.Model
{
    public class Totp
    {
        public long Id { get; set; }

        /// <summary>
        /// Token para ser buscado
        /// </summary>
        public string HashTopt { get; set; }

        /// <summary>
        /// Token para ser buscado
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// operação
        /// </summary>
        public string ConsumeToken { get; set; }

        /// <summary>
        /// Tempo de expiracao em segundos
        /// </summary>
        public long TimeToLive { get; set; }

        /// <summary>
        /// Data de criançao
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Foi Deletado
        /// </summary>
        public bool Deleted { get; set; }

        public bool IsExpired()
        {
            var expiration = Created.ToUniversalTime().AddSeconds(TimeToLive);
            var now = DateTime.UtcNow;

            if (expiration.Date.CompareTo(now.Date) != 0)
                return true;
            return (expiration.TimeOfDay.TotalSeconds - now.TimeOfDay.TotalSeconds) < 0;
        }
    }
}