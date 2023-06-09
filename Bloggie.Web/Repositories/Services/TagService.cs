﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.Services
{
    public class TagService : ITagInterface
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public TagService(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                _bloggieDbContext.Tags.Remove(existingTag);
                await _bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _bloggieDbContext.Tags.ToListAsync();

            return tags;
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            return await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes
                await _bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
