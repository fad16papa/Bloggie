﻿using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IBlogPostInterface : IBaseInterface<BlogPost>
    {
        Task<BlogPost> GetByUrlHandeAsync(string urlHandle);
    }
}
