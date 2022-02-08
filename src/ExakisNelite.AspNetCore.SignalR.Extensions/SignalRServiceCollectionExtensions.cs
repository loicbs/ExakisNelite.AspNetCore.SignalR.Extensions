using ExakisNelite.AspNetCore.SignalR.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SignalRServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the object identifier based user id provider for SignalR.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="services"/> is a null reference.</exception>
        public static IServiceCollection AddObjectIdentifierBasedUserIdProvider(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddClaimsPrincipalBasedUserIdProvider(ClaimsPrincipalExtensions.GetUserId);

            return services;
        }

        /// <summary>
        /// Adds the email address based user id provider for SignalR.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="services"/> is a null reference.</exception>
        public static IServiceCollection AddEmailBasedUserIdProvider(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddClaimsPrincipalBasedUserIdProvider(ClaimsPrincipalExtensions.GetEmail);

            return services;
        }

        /// <summary>
        /// Adds the claims principal based user id provider for SignalR.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="userIdAccessor">The function to define a user id from the <see cref="ClaimsPrincipal"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddClaimsPrincipalBasedUserIdProvider(
            this IServiceCollection services,
            Func<ClaimsPrincipal, string?> userIdAccessor
        )
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IUserIdProvider>(new ClaimsPrincipalBasedUserIdProvider(userIdAccessor));

            return services;
        }
    }
}
