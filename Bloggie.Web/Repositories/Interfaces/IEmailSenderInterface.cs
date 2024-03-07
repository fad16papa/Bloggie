namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IEmailSenderInterface
    {
        Task SendEmailAasync(string to, string from, string body);
    }
}