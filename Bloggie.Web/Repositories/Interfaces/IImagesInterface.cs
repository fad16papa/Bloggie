namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IImagesInterface
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
