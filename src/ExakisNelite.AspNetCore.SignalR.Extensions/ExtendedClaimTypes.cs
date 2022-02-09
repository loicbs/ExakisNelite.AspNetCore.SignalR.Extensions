namespace ExakisNelite.AspNetCore.SignalR.Extensions
{
    /// <summary>
    /// Extended claim types.
    /// </summary>
    public static class ExtendedClaimTypes
    {
        /// <summary>
        /// Object identifier claim type.
        /// </summary>
#pragma warning disable S5332
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
#pragma warning restore S5332
        /// <summary>
        /// Subject claim type.
        /// </summary>
        public const string Subject = "sub";
    }
}
