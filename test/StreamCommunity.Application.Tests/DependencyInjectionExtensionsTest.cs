using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StreamCommunity.Application.Common;

namespace StreamCommunity.Application.Tests
{
    [TestFixture]
    public class DependencyInjectionExtensionsTest
    {
        #region AddTwitchCommunityApplication()

        [Test]
        [TestCase(typeof(IMediator))]
        [TestCase(typeof(IDateTimeProvider))]
        public void AddTwitchCommunityApplication_TypeIsRegistered(Type type)
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddTwitchCommunityApplication(configuration);

            // Assert
            var provider = services.BuildServiceProvider();
            Assert.IsNotNull(provider.GetRequiredService(type));
        }

        #endregion
    }
}