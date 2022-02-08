using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ExakisNelite.AspNetCore.SignalR.Extensions
{
    /// <summary>
    /// A provider that returns a user ID based on the claims of the current user.
    /// </summary>
    public class ClaimsPrincipalBasedUserIdProvider : DefaultUserIdProvider
    {
        private readonly Func<ClaimsPrincipal, string?> _userIdAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsPrincipalBasedUserIdProvider"/> class.
        /// </summary>
        /// <param name="userIdAccessor">The method to retrieve the user Id from the <see cref="ClaimsPrincipal"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="userIdAccessor"/> is a null reference.</exception>
        public ClaimsPrincipalBasedUserIdProvider(Func<ClaimsPrincipal, string?> userIdAccessor)
        {
            _userIdAccessor = userIdAccessor ?? throw new ArgumentNullException(nameof(userIdAccessor));
        }

        /// <summary>
        /// Gets the user ID for the specified connection.
        /// </summary>
        /// <param name="connection">The connection to get the user ID for.</param>
        /// <remarks>
        /// If the authenticated user does not have userd Id, this method falls back on the <see cref="DefaultUserIdProvider"/>.
        /// </remarks>
        /// <returns>The user ID for the specified connection.</returns>
        public override string GetUserId(HubConnectionContext connection)
        {
            return _userIdAccessor(connection.User) ?? base.GetUserId(connection);
        }
    }
}
