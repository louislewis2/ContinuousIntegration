namespace ContinuousIntegrationUnitTests
{
    using Xunit;

    using UnitTestBase;
    using ContinuousIntegration;

    public class CalculatorUnitTests : UnitTestBase
    {
        #region Test Methods

        [Fact]
        public void Calculator_Can_Add()
        {
            // Arrange
            var calculator = new Calculator();
            var x = 22.0;
            var y = 22.2;
            var expected = 44.2;

            // Act
            var result = calculator.Add(x, y);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculator_Can_Subtract()
        {
            // Arrange
            var calculator = new Calculator();
            var x = 22.0;
            var y = 22.2;
            var expected = 0.2;

            // Act
            var result = calculator.Subtract(x, y);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion Test Methods
    }
}
