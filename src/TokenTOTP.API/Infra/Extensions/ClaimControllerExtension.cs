using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TokenTOTP.API.Infra.Extensions
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