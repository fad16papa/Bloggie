using Bloggie.Web.Repositories.Interfaces;

namespace Bloggie.Web.Repositories.Services
{
    public class ImagesService : IImagesInterface
    {
        public Task<string> UploadAsync(IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}
