using Microsoft.AspNetCore.Components;
using PortfolioWebsite.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
    public class ProjectService
    {
		//having some weird issues with the client disposing immediately
        private static HttpClient _http { get {  
				var a = new HttpClient();
				a.BaseAddress = new Uri(baseUri);
				return a;
			} }
		private static string baseUri = "";
		public ProjectService(HttpClient http,NavigationManager navigationManager)
        {
			baseUri = navigationManager.BaseUri;
			//baseUri = http.BaseAddress.ToString();
		}
		public async Task<List<Project>> GetProjects()
		{
			List<Project> result = new List<Project>();
			try
			{
				result = await _http.GetFromJsonAsync<List<Project>>("api/Project/Index");
				_http.Dispose();
				return result;
			}
			catch(Exception ex)
			{
				Console.WriteLine(String.Format("Error fetching Projects + {0}", ex.Message));
				return result;
			}
		}
		public async Task<Project> GetProject(string id)
		{
			var result = await _http.GetFromJsonAsync<Project>($"api/Project/{id}");
			_http.Dispose();
			return result;
		}
		public async Task<Project> CreateProject(Project project)
		{
			var response = await _http.PostAsJsonAsync("api/Project/Create", project);
			_http.Dispose();
			return await response.Content.ReadFromJsonAsync<Project>();
		}
		public async Task<Project> UpdateProject(Project project)
		{
			var response = await _http.PutAsJsonAsync("api/ProjectUpdate", project);
			_http.Dispose();
			return await response.Content.ReadFromJsonAsync<Project>();
		}
		public async Task DeleteProject(string id)
		{
			await _http.DeleteAsync($"api/Project/Delete/{id}");
			_http.Dispose();
		}
    }
}
