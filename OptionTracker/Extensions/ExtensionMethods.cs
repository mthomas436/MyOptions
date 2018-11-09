using Microsoft.AspNetCore.Identity;
using OptionTracker.Models;
using System.Linq;
using System.Security.Claims;


namespace OptionTracker.Extensions
{
    public static class ExtensionMethods
    {
        public static string GetCurrentUserFullName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == "FirstName");
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == "LastName");
            return firstName?.Value +  lastName?.Value;
        }
 
    }
}