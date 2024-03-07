using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageUploadController : Controller
	{
		[HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload()
		{
			// get the file from the request
			var file = Request.Form.Files[0];
			// get the path to the server's /images folder
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
			// create a unique file name
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			// create the full path to the file
			var fullPath = Path.Combine(folderPath, fileName);
			// create a stream to write the file to
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				// write the file to the stream
				await file.CopyToAsync(stream);
			}
			// return the path to the uploaded image
			return Ok($"/images/{fileName}");
		}	
	}
}
