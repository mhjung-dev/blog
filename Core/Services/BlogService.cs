﻿using Blog.Core.Entities;
using Markdig;
using System.Net.Http.Json;

namespace Blog.Core.Services
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;
#if DEBUG
        private readonly string _postsUrl = "posts/";
#else
        private readonly string _postsUrl = "https://raw.githubusercontent.com/mhjung-dev/blog/main/wwwroot/posts/";
#endif


        public BlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BlogPost>> GetPostsAsync()
        {
            var posts = new List<BlogPost>();

            // Here you might want to fetch a JSON file that lists all posts metadata
            var postFiles = await _httpClient.GetFromJsonAsync<List<string>>(_postsUrl + "posts.json");

            foreach (var postFile in postFiles)
            {
                var post = await GetPostAsync(postFile);
                posts.Add(post);
            }

            return posts;
        }

        public async Task<BlogPost> GetPostAsync(string fileName)
        {
            var markdown = await _httpClient.GetStringAsync(_postsUrl + fileName);
            var content = Markdown.ToHtml(markdown);

            // Assuming the file name is in the format 'yyyy-MM-dd-title.md'
            var parts = fileName.Split(new[] { '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
            var date = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
            var title = string.Join(' ', parts[3..]);

            return new BlogPost
            {
                Title = title,
                Date = date,
                Content = content
            };
        }
    }
}
