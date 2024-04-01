using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Shared.Repository;
using PortfolioWebsite.Shared.Models;
using PortfolioWebsite.DataAccess;

namespace PortfolioWebsite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		ProjectRepository _projectRepository = new ProjectRepository();

		[HttpGet]
		[Route("Index")]
		public async Task<ActionResult<List<Project>>> Index()
		{
			var projects = await _projectRepository.GetAll();
			return projects;
		}

		[HttpPost]
		[Route("Create")]
		public async void Create(Project project)
		{
			await _projectRepository.Create(project);
		}

		[HttpGet]
		[Route("Project/{id}")]
		public async Task<ActionResult<Project>> Details(string id)
		{
			return await _projectRepository.GetByID(id);
		}

		[HttpPut]
		[Route("Edit")]
		public async void Edit(Project project)
		{
			await _projectRepository.Update(project);
		}

		[HttpDelete]
		[Route("Delete/{id}")]
		public async void Delete(string id)
		{
			await _projectRepository.Delete(id);
		}

	}
}
