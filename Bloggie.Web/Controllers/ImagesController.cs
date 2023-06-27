using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesInterface _imagesInterface;

        public ImagesController(IImagesInterface imagesInterface)
        {
            _imagesInterface = imagesInterface;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile) 
        {
            await _imagesInterface.UploadAsync(formFile);

            return Ok("This is a test");
        }
    }
}
