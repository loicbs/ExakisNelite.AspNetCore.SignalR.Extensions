using ExakisNelite.AspNetCore.SignalR.Extensions;

namespace System.Security.Claims
{
    /// <summary>
    /// Extensions for <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user ID claim value.
        /// </summary>
        /// <param name="principal">The user principal.</param>
        /// <returns>The user ID claim value.</returns>
        /// <remarks>
        /// Claims taken is in order :
        /// - http://schemas.microsoft.com/identity/claims/objectidentifier
        /// - http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
        /// - sub
        /// - http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="principal"/> is a null reference.</exception>
        public static string? GetUserId(this ClaimsPrincipal principal)
            => principal.GetClaim(ExtendedClaimTypes.ObjectIdentifier, ClaimTypes.NameIdentifier, ExtendedClaimTypes.Subject, ClaimTypes.Name);

        /// <summary>
        /// Gets the email claim value.
        /// </summary>
        /// <param name="principal">The user principal.</param>
        /// <returns>The email claim value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="principal"/> is a null reference.</exception>
        public static string? GetEmail(this ClaimsPrincipal principal)
            => principal.GetClaim(ClaimTypes.Email);

        private static string? GetClaim(this ClaimsPrincipal principal, params string[] claimTypes)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            foreach (var claimType in claimTypes)
            {
                var claim = principal.FindFirst(claimType);
                if (claim != null)
                {
                    return claim.Value;
                }
            }
            return null;
        }
    }
}