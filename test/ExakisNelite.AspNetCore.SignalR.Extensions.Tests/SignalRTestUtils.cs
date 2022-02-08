using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace ExakisNelite.AspNetCore.SignalR.Extensions.Tests
{
    public static class SignalRTestUtils
    {
        public static HubConnectionContext CreateHubConnectionContext(ConnectionContext connection, string? userIdentifier = null)
        {
            return new HubConnectionContext(connection, TimeSpan.FromSeconds(15), NullLoggerFactory.Instance)
            {
                Protocol = new JsonHubProtocol(),
                UserIdentifier = userIdentifier,
            };
        }
    }
}
