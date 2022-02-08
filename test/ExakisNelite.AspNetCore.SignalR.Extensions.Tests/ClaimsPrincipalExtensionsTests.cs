using System;
using System.Security.Claims;
using Xunit;

namespace ExakisNelite.AspNetCore.SignalR.Extensions.Tests
{
    public class ClaimsPrincipalExtensionsTests
    {
        #region GetUserId

        [Fact]
        public void GetUserId_ThrowsAnArgumentNullException_WhenPrincipalIsNull()
        {
            // Arrange
            ClaimsPrincipal principal = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => principal.GetUserId());
        }

        [Theory]
        [InlineData(ExtendedClaimTypes.ObjectIdentifier, "ObjectIdentifier")]
        [InlineData(ClaimTypes.NameIdentifier, "NameIdentifier")]
        [InlineData(ExtendedClaimTypes.Subject, "Subject")]
        [InlineData(ClaimTypes.Name, "Name")]
        public void GetUserId_ReturnsTheCorrectClaim(string claimType, string claimValue)
        {
            // Arrange
            var principal = new TestPrincipal(new Claim(claimType, claimValue));

            // Act
            var actual = principal.GetUserId();

            // Assert
            Assert.Equal(claimValue, actual);
        }

        [Fact]
        public void GetUserId_ReturnsNull_WhenThereIsNoAppropriateClaim()
        {
            // Arrange
            var principal = new TestPrincipal();

            // Act
            var actual = principal.GetUserId();

            // Assert
            Assert.Null(actual);
        }

        #endregion

        #region GetEmail

        [Fact]
        public void GetEmail_ThrowsAnArgumentNullException_WhenPrincipalIsNull()
        {
            // Arrange
            ClaimsPrincipal principal = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => principal.GetEmail());
        }

        [Theory]
        [InlineData(ClaimTypes.Email, "Email")]
        public void GetEmail_ReturnsTheCorrectClaim(string claimType, string claimValue)
        {
            // Arrange
            var principal = new TestPrincipal(new Claim(claimType, claimValue));

            // Act
            var actual = principal.GetEmail();

            // Assert
            Assert.Equal(claimValue, actual);
        }

        [Fact]
        public void GetEmail_ReturnsNull_WhenThereIsNoAppropriateClaim()
        {
            // Arrange
            var principal = new TestPrincipal();

            // Act
            var actual = principal.GetEmail();

            // Assert
            Assert.Null(actual);
        }

        #endregion
    }
}