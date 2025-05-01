using TaskManager.Core.Entities;
using TaskManager.Core.Enums;

namespace TaskManager.UnitTests.Entities
{
    public class ResultViewModelTests
    {
        [Fact]
        public void Constructor_ShouldInitializeTaskEntityWithDefaultValues()
        {
            // Arrange Act

            var entity = ResultViewModel.Success();

            // Assert
            Assert.True(entity.IsSuccess);
            Assert.Empty(entity.Message);
            
        }

        [Fact]
        public void Constructor_ShouldInitializeTaskEntityWithDefaultValues2()
        {
            // Arrange Act

            var entity = ResultViewModel.Error("error teste");

            // Assert
            Assert.False(entity.IsSuccess);
            Assert.Equal("error teste", entity.Message);

        }

        [Fact]
        public void Constructor_ShouldInitializeTaskEntityWithDefaultValues3()
        {
            // Arrange Act

            var entity = ResultViewModel<string>.Error("error teste");

            // Assert
            Assert.False(entity.IsSuccess);
            Assert.Equal("error teste", entity.Message);

        }

        [Fact]
        public void Constructor_ShouldInitializeTaskEntityWithDefaultValues4()
        {
            // Arrange Act

            var entity = ResultViewModel<string>.Success("Success teste");

            // Assert
            Assert.True(entity.IsSuccess);
            Assert.Equal("Success teste", entity.Data);

        }
    }
}
