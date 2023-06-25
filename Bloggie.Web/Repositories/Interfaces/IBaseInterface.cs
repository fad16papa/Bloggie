using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IBaseInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<Tag> DeleteAsync(Guid id);
    }
}
