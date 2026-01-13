using HE.Remediation.Core.Extensions;

namespace HE.Remediation.Core.Tests.Extensions;

public class DateSlippageExtensionTests
{
    [Fact]
    public void HasChanged_ShouldReturnFalse_WhenPreviousDateIsNull()
    {
        // Arrange
        DateTime? previousDate = null;
        DateTime? newDate = new DateTime(2025, 6, 15);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnTrue_WhenPreviousDateHasValueButNewDateIsNull()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2025, 6, 15);
        DateTime? newDate = null;

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnFalse_WhenBothDatesAreNull()
    {
        // Arrange
        DateTime? previousDate = null;
        DateTime? newDate = null;

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnTrue_WhenMonthChanges()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2025, 6, 15);
        DateTime? newDate = new DateTime(2025, 7, 15);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnTrue_WhenYearChanges()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2024, 6, 15);
        DateTime? newDate = new DateTime(2025, 6, 15);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnFalse_WhenMonthAndYearAreSame()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2025, 6, 15);
        DateTime? newDate = new DateTime(2025, 6, 20);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnFalse_WhenDatesAreExactlySame()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2025, 6, 15);
        DateTime? newDate = new DateTime(2025, 6, 15);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasChanged_ShouldReturnTrue_WhenBothMonthAndYearChange()
    {
        // Arrange
        DateTime? previousDate = new DateTime(2024, 6, 15);
        DateTime? newDate = new DateTime(2025, 7, 15);

        // Act
        var result = previousDate.HasChanged(newDate);

        // Assert
        Assert.True(result);
    }
}
