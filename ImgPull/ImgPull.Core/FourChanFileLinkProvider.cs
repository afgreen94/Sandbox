using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ImgPull.Core
{
    public class FourChanFileLinkProvider : IFileLinkProvider
    {
        private const string CatalogJsonSourceUrl = "https://a.4cdn.org/{0}/catalog.json";
        private const string ThreadJsonSourceUrl = "https://a.4cdn.org/{0}/thread/{1}.json";
        private const string ImageSourceUrlTemplate = "https://i.4cdn.org/{0}/{1}";

        private readonly IFileDownloader downloader;
        private readonly string board;
        private readonly ThreadFilter threadFilter;

        public FourChanFileLinkProvider(IFileDownloader downloader) { this.downloader = downloader; }
        public FourChanFileLinkProvider(IFileDownloader downloader, Options options) : this(downloader) 
        {
            this.board = options.Board;

            if(options.IncludedExpressions.Count != 0)
                this.threadFilter = new ThreadFilter(options.IncludedExpressions);
        }

        public async IAsyncEnumerable<FileLinkResult> GetLinksAsync(FileLinkProviderArgs optionsOVerride = null)
        {
            var url = string.Format(CatalogJsonSourceUrl, this.board);
            var catalogPages = await this.RetrieveModelsAsync<List<PageModel>>(url).ConfigureAwait(false);

            foreach (var page in catalogPages)
                foreach (var thread in page.Threads)
                    if(this.threadFilter == default || this.threadFilter.IncludeThread(thread))
                        await foreach (var threadFileLink in this.RetrieveThreadFileLinksAsync(thread).ConfigureAwait(false))
                            yield return threadFileLink;
        }

        private async IAsyncEnumerable<FileLinkResult> RetrieveThreadFileLinksAsync(ThreadModel thread)
        {
            var threadPosts = await this.RetrieveModelsAsync<PostListModel>(string.Format(ThreadJsonSourceUrl, this.board, thread.Id)).ConfigureAwait(false);

            foreach(var post in threadPosts.Posts)
            {
                if (!this.TryGetFileLinkResult(post, out var fileLinkResult))
                    continue;

                yield return fileLinkResult;
            }
        }

        private async Task<T> RetrieveModelsAsync<T>(string url)
        {
            var res = await this.downloader.DownloadAsync(url).ConfigureAwait(false);
            var json = await res.AsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(json);
        }

        private bool TryGetFileLinkResult(PostModel post, out FileLinkResult result)
        {
            if(!string.IsNullOrEmpty(post.FileName) && !string.IsNullOrEmpty(post.Extension))
            { 
                result = new FileLinkResult()
                {
                    FileLink = string.Format(ImageSourceUrlTemplate, this.board, string.Concat(post.InternalId, post.Extension)),
                    FileName = string.Concat(post.FileName, post.Extension)
                };

                return true;
            }

            result = default;
            return false;
        }

        public class Options
        {
            public string Board { get; set; }
            public HashSet<string> IncludedExpressions { get; set; } = new();
        }
    }

    public class PageModel
    {
        [JsonPropertyName("page")]
        public int PageNumber { get; set; }

        [JsonPropertyName("threads")]
        public IList<ThreadModel> Threads { get; set; } = new List<ThreadModel>();
    }

    public class PostListModel
    {
        [JsonPropertyName("posts")]
        public IList<PostModel> Posts { get; set; } = new List<PostModel>();
    }

    public class PostModel
    {
        [JsonPropertyName("no")]
        public long Id { get; set; }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [JsonPropertyName("ext")]
        public string Extension { get; set; }

        [JsonPropertyName("tim")]
        public long InternalId { get; set; }
    }

    public class ThreadModel : PostModel
    {
        [JsonPropertyName("sub")]
        public string Name { get; set; }

        [JsonPropertyName("com")]
        public string Description { get; set; }

        [JsonPropertyName("last_replies")]
        public IList<PostModel> Replies { get; set; } = new List<PostModel>();
    }


    public class ThreadFilter
    {
        private HashSet<string> expressions = new();

        public ThreadFilter(HashSet<string> expressions) 
        {
            foreach (var e in expressions)
                this.expressions.Add(e.ToUpper());
        }

        public bool IncludeThread(ThreadModel thread)
        {

            foreach (var e in this.expressions)
            {
                if (!string.IsNullOrEmpty(thread.Name) && thread.Name.ToUpper().Contains(e))
                    return true;
                if (!string.IsNullOrEmpty(thread.Description) && thread.Description.ToUpper().Contains(e))
                    return true;
            }

            return false;
        }
    }
}
