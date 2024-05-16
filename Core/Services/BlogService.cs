using Blog.Core.Entities;

namespace Blog.Core.Services
{
    public class BlogService
    {
        public Task<List<BlogPost>> GetPostsAsync()
        {
            // Replace with actual data fetching logic
            return Task.FromResult(new List<BlogPost>
            {
                new BlogPost { Title = "First Post", Date = DateTime.Now, Content = "Content of the first post" }
            });
        }
    }
}
