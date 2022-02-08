using Microsoft.AspNetCore.Connections;
using System;
using System.Security.Claims;
using Xunit;

namespace ExakisNelite.AspNetCore.SignalR.Extensions.Tests
{
    public class ClaimsPrincipalBasedUserIdProviderTests
    {
        #region Constructor

        [Fact]
        public void Constructor_ThrowsAnArgumentNullException_WhenUserIdAccessorIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ClaimsPrincipalBasedUserIdProvider(null!));
        }

        #endregion

        #region GetUserId

        [Fact]
        public void GetUserId_ReturnsUserIdAccessorValue()
        {
            // Arrange 
            var expected = Guid.NewGuid().ToString();
            var userIdAccessor = (ClaimsPrincipal claimsPrincipal) => expected;

            var connectionContext = new DefaultConnectionContext
            {
                User = new TestPrincipal()
            };

            var hubConnectionContext = SignalRTestUtils.CreateHubConnectionContext(connectionContext);

            var sut = new ClaimsPrincipalBasedUserIdProvider(userIdAccessor);

            // Act
            var actual = sut.GetUserId(hubConnectionContext);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUserId_ReturnsDefaultValue_WhenAccessorReturnsNull()
        {
            // Arrange 
            var expected = Guid.NewGuid().ToString();
            var userIdAccessor = (ClaimsPrincipal claimsPrincipal) => (string)null!;

            var connectionContext = new DefaultConnectionContext
            {
                User = new TestPrincipal(new Claim(ClaimTypes.NameIdentifier, expected))
            };

            var hubConnectionContext = SignalRTestUtils.CreateHubConnectionContext(connectionContext);

            var sut = new ClaimsPrincipalBasedUserIdProvider(userIdAccessor);

            // Act
            var actual = sut.GetUserId(hubConnectionContext);

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}