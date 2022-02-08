using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using Xunit;

namespace ExakisNelite.AspNetCore.SignalR.Extensions.Tests
{
    public class SignalRServiceCollectionExtensionsTests
    {
        #region AddObjectIdentifierBasedUserIdProvider

        [Fact]
        public void AddObjectIdentifierBasedUserIdProvider_ThrowsAnArgumentNullException_WhenServicesIsNull()
        {
            // Arrange
            ServiceCollection services = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => services.AddObjectIdentifierBasedUserIdProvider());
        }

        [Fact]
        public void AddObjectIdentifierBasedUserIdProvider_AddsWithCorrectLifetime()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act 
            services.AddObjectIdentifierBasedUserIdProvider();

            // Assert
            var descriptor = services[0];
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
            Assert.Equal(typeof(IUserIdProvider), descriptor.ServiceType);
        }

        [Fact]
        public void AddObjectIdentifierBasedUserIdProvider_AddsCorrectUserIdProviderLifetime()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var services = new ServiceCollection();
            services.AddObjectIdentifierBasedUserIdProvider();
            var serviceProvider = services.BuildServiceProvider();


            var connectionContext = new DefaultConnectionContext
            {
                User = new TestPrincipal(new Claim(ExtendedClaimTypes.ObjectIdentifier, expected))
            };
            var hubConnectionContext = SignalRTestUtils.CreateHubConnectionContext(connectionContext);

            // Act 
            var sut = serviceProvider.GetRequiredService<IUserIdProvider>();
            var actual = sut.GetUserId(hubConnectionContext);

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion

        #region AddEmailBasedUserIdProvider

        [Fact]
        public void AddEmailBasedUserIdProvider_ThrowsAnArgumentNullException_WhenServicesIsNull()
        {
            // Arrange
            ServiceCollection services = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => services.AddEmailBasedUserIdProvider());
        }

        [Fact]
        public void AddEmailBasedUserIdProvider_AddsWithCorrectLifetime()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act 
            services.AddEmailBasedUserIdProvider();

            // Assert
            var descriptor = services[0];
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
            Assert.Equal(typeof(IUserIdProvider), descriptor.ServiceType);
        }

        [Fact]
        public void AddEmailBasedUserIdProvider_AddsCorrectUserIdProviderLifetime()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var services = new ServiceCollection();
            services.AddEmailBasedUserIdProvider();
            var serviceProvider = services.BuildServiceProvider();


            var connectionContext = new DefaultConnectionContext
            {
                User = new TestPrincipal(new Claim(ClaimTypes.Email, expected))
            };
            var hubConnectionContext = SignalRTestUtils.CreateHubConnectionContext(connectionContext);

            // Act 
            var sut = serviceProvider.GetRequiredService<IUserIdProvider>();
            var actual = sut.GetUserId(hubConnectionContext);

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion

        #region AddClaimsPrincipalBasedUserIdProvider

        [Fact]
        public void AddClaimsPrincipalBasedUserIdProvider_ThrowsAnArgumentNullException_WhenServicesIsNull()
        {
            // Arrange
            ServiceCollection services = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => services.AddClaimsPrincipalBasedUserIdProvider(_ => string.Empty));
        }

        [Fact]
        public void AddClaimsPrincipalBasedUserIdProvider_AddsWithCorrectLifetime()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act 
            services.AddClaimsPrincipalBasedUserIdProvider(_ => string.Empty);

            // Assert
            var descriptor = services[0];
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
            Assert.Equal(typeof(IUserIdProvider), descriptor.ServiceType);
        }

        [Fact]
        public void AddClaimsPrincipalBasedUserIdProvider_AddsCorrectUserIdProviderLifetime()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var services = new ServiceCollection();
            services.AddClaimsPrincipalBasedUserIdProvider(_ => expected);
            var serviceProvider = services.BuildServiceProvider();


            var connectionContext = new DefaultConnectionContext
            {
                User = new TestPrincipal()
            };
            var hubConnectionContext = SignalRTestUtils.CreateHubConnectionContext(connectionContext);

            // Act 
            var sut = serviceProvider.GetRequiredService<IUserIdProvider>();
            var actual = sut.GetUserId(hubConnectionContext);

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}