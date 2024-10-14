using JernaClassLib.Components;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using JernaClassLib;
using JernaClassLib.IServices;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace JernaUnitTests
{
    public class CoachingUnitTests : TestContext
    {
        [Fact]
        public void ChangeKidAmount_WhenValidNumber_UpdatesTempChildrensAge()
        {
            // Arrange

            var coachingComponent = new Coaching();
            var args = new ChangeEventArgs { Value = 3 };

            // Act
            coachingComponent.ChangeKidAmount(args);

            // Assert
            Assert.Equal(3, coachingComponent.tempChildrensAge.Length);
        }

        [Fact]
        public void ChangeKidAmount_WhenNegativeNumber_DoesNotUpdateTempChildrensAge()
        {
            // Arrange
            var coachingComponent = new Coaching();
            var args = new ChangeEventArgs { Value = -1 };

            // Act
            coachingComponent.ChangeKidAmount(args);

            // Assert
            Assert.Empty(coachingComponent.tempChildrensAge); 
        }

        [Fact]
        public void ChangeKidAmount_WhenNumberGreaterThanTen_DoesNotUpdateTempChildrensAge()
        {
            // Arrange
            var coachingComponent = new Coaching();
            var args = new ChangeEventArgs { Value = 11 };

            // Act
            coachingComponent.ChangeKidAmount(args);

            // Assert
            Assert.Empty(coachingComponent.tempChildrensAge); 
        }

        [Fact]
        public void ChangeKidAmount_WhenNullValue_DoesNotUpdateTempChildrensAge()
        {
            // Arrange
            var coachingComponent = new Coaching();
            var args = new ChangeEventArgs { Value = null };

            // Act
            coachingComponent.ChangeKidAmount(args);

            // Assert
            Assert.Empty(coachingComponent.tempChildrensAge); 
        }
    }
}
