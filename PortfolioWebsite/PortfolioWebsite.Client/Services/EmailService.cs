using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
	public class EmailService
	{
		private static HttpClient _httpClient
		{
			get
			{
				var a = new HttpClient();
				a.BaseAddress = new Uri(baseUri);
				return a;
			}
		}
		private static string baseUri = "";
		public EmailService(HttpClient http, NavigationManager navigationManager)
		{
			baseUri = navigationManager.BaseUri;
		}

		public async Task SendEmail(string subject, string message,string sender)
		{
			var formData = new MultipartFormDataContent();
			formData.Add(new StringContent(subject), "subject");
			formData.Add(new StringContent(message), "message");
			formData.Add(new StringContent(sender), "sender");
			var response = await _httpClient.PostAsync("api/Email/SendEmail", formData);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception("Failed to send email");
			}
		}
	}
}
