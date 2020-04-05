using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TokenTOTP.Infra.Extensions
{
    public static class ClaimControllerExtension
    {
        public static string GetDataFromClaims(this ControllerBase data, string claimsName)
        {
            var claim = data.User.Claims.FirstOrDefault(x => x.Type.EndsWith(claimsName));
            return claim is null ? "" : claim.Value;
        }
    }
}