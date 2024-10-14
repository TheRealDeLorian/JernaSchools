using Bunit;
using JernaClassLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using JernaClassLib;
using JernaClassLib.IServices;
using JernaClassLib.Data.DatabaseObjects;

namespace JernaUnitTests
{
    public class LoginUnitTests : TestContext
    {
        [Fact]
        public void ToggleInputOption_ShouldToggleInputOptionState()
        {
            // Arrange
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockEmailService = new Mock<IEmailService>();
            Services.AddSingleton(mockEmailService.Object);
            var mockSSService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSSService.Object);


            var component = RenderComponent<Login>();

            component.Instance.ToggleInputOption();

            Assert.True(component.Instance.inputOption);

            component.Instance.ToggleInputOption();

            Assert.False(component.Instance.inputOption);
        }

        [Fact]
        public async Task VerifyEmailAsync_InvalidEmail_ShouldSetEmailErrorMessage()
        {
            // Arrange
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockEmailService = new Mock<IEmailService>();
            Services.AddSingleton(mockEmailService.Object);
            var mockSSService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSSService.Object);
            var component = RenderComponent<Login>();

            component.Instance.userEmail = "invalidEmail";

            // Act
            await component.Instance.VerifyEmailAsync();

            // Assert - message should be set to emailError
            Assert.Equal("Please input a valid email address.", component.Instance.message);
        }

        [Fact]
        public async Task VerifyEmailAsync_ValidEmail_ShouldSendEmailAndSetSuccessMessage()
        {
            // Arrange
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockSSService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSSService.Object);
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendAuthEmailAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            Services.AddSingleton(mockEmailService.Object);
            var component = RenderComponent<Login>();

            component.Instance.userEmail = "user@example.com";

            // Act
            await component.Instance.VerifyEmailAsync();

            // Assert 
            Assert.Equal("We just sent you an email! Enter temporary code here.", component.Instance.message);
            Assert.True(component.Instance.inputOption);
        }


        [Fact]
        public async Task VerifyCodeAsync_InvalidCode_ShouldSetInvalidCodeMessage()
        {
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockEmailService = new Mock<IEmailService>();
            Services.AddSingleton(mockEmailService.Object);
            var mockSSService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSSService.Object);
            // Arrange
            var component = RenderComponent<Login>();

            component.Instance.inputCode = "1234"; 

            // Act
            await component.Instance.VerifyCodeAsync();

            // Assert 
            Assert.Equal("Invalid temporary code.", component.Instance.message);
        }

        [Fact]
        public async Task VerifyCodeAsync_ValidCode_ShouldAuthenticateSuccessfully()
        {
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockEmailService = new Mock<IEmailService>();
            Services.AddSingleton(mockEmailService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockSecureStorageService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSecureStorageService.Object);

            mockAuthService.Setup(m => m.VerifyTempReturnAuthAsync(It.IsAny<string>()))
                .ReturnsAsync("AUTH_CODE_123456");
            mockSecureStorageService.Setup(m => m.StoreKeyValueAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            Services.AddSingleton(mockAuthService.Object);
            Services.AddSingleton(mockSecureStorageService.Object);

            var component = RenderComponent<Login>();

            component.Instance.inputCode = "1234567890123456"; 

            // Act
            await component.Instance.VerifyCodeAsync();

            // Assert 
            Assert.Equal("Authentication successful.", component.Instance.message);
            Assert.False(component.Instance.inputOption);
        }

        [Fact]
        public async Task Logout_ShouldSetRandomUserErrorMessage()
        {
            // Arrange
            var mockEventService = new Mock<EventService>();
            Services.AddSingleton(mockEventService.Object);
            var mockEmailService = new Mock<IEmailService>();
            Services.AddSingleton(mockEmailService.Object);
            var mockAuthService = new Mock<IAuthService>();
            Services.AddSingleton(mockAuthService.Object);
            var mockSecureStorageService = new Mock<ISecureStorageService>();
            Services.AddSingleton(mockSecureStorageService.Object);
            var component = RenderComponent<Login>();

            component.Instance.user = new User { Username = "Random User" };

            // Act
            await component.Instance.Logout();

            // Assert 
            Assert.Equal("Cannot log out of a random user account. To login to a different account, just log in as your desired account by entering their email and getting the temporary code.", component.Instance.message);
        }

       
    }
}
