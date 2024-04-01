using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PortfolioWebsite.Client.Services
{
	/// <summary>
	/// service for uploading IBrowserFile files to the server, saving to server's /images folder
	/// </summary>
	public class ImageUploadService
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
		public ImageUploadService(HttpClient http, NavigationManager navigationManager)
		{
			baseUri = navigationManager.BaseUri;
		}

		/// <summary>
		/// Uploads an image to the server
		/// </summary>
		/// <param name="file">The image file to upload</param>
		/// <returns>The path to the uploaded image</returns>
		public async Task<string> UploadImage(IBrowserFile file)
		{
			// create a form data object
			var formData = new MultipartFormDataContent();
		 																		
			// add the image file to the form data
			formData.Add(new StreamContent(file.OpenReadStream(maxAllowedSize:1024*30000)), "file", file.Name);
			
			// send the form data to the server
			var response = await _httpClient.PostAsync("api/ImageUpload/Upload", formData);
 			
			// get the path to the uploaded image from the response
			var imagePath = await response.Content.ReadAsStringAsync();
 									
			// return the path to the uploaded image
			return imagePath;
		}

		public async Task<string> UploadImages(List<IBrowserFile> files)
		{
			var imagePaths = new List<string>();
			foreach (var file in files)
			{
				imagePaths.Add(await UploadImage(file));
			}
			return JsonSerializer.Serialize(imagePaths);
		}
	}
}
