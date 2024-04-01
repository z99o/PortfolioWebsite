using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using PortfolioWebsite.Controllers;

public class EmailControllerTests
{
    private readonly IConfiguration _config;
    [Fact]
    public void SendEmail_ReturnsTrue_WhenEmailIsSentSuccessfully()
    {
        // Arrange
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(c => c["SMTPClient:Username"]).Returns("z99ono@gmail.com");
        mockConfig.Setup(c => c["SMTPClient:Password"]).Returns("rrmf nbou urag zegz");
        mockConfig.Setup(c => c["SMTPClient:Destination"]).Returns("zminbox@protonmail.me");

        var controller = new EmailController(mockConfig.Object);

        // Act
        var result = controller.SendEmail("Test Subject", "Test Body", "Test Sender");

        // Assert
        Assert.True(result);
    }
}
