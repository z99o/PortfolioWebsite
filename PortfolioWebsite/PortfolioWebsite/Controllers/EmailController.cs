using System.Net.Mail;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController :Controller
	{

		private readonly IConfiguration _config;
		public EmailController(IConfiguration config)
		{
			_config = config;
		}
		/// <summary>
		/// Sends an email
		/// </summary>
		/// <param name="to">The email address to send the email to</param>
		/// <param name="subject">The subject of the email</param>
		/// <param name="body">The body of the email</param>
		/// <returns>True if the email was sent successfully, false otherwise</returns>
		[HttpPost]
		[Route("SendEmail")]
		public async Task<IActionResult> SendEmail()
		{
			try
			{
				// Create a new SmtpClient
				string sender = Request.Form["sender"];
				string subject = Request.Form["subject"];
				string body = Request.Form["message"];
				using (var client = new SmtpClient("smtp.gmail.com"))
				{
					// Set the port to 587
					client.Port = 587;
					// Enable SSL
					client.EnableSsl = true;
					// Set the credentials
					client.Credentials = new System.Net.NetworkCredential(_config["SMTPClient:Username"], _config["SMTPClient:Password"]);
					// Create a new MailMessage
					await client.SendMailAsync(sender, _config["SMTPClient:Destination"], subject, body);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sending email: " + ex.Message);
				return Ok(false);
			}
			return Ok(true);
		}
	}
}
