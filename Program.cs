using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace holding
{
  class Program
  {
    private static readonly HttpClient client = new HttpClient();
    private static readonly List<Post> posts = new List<Post>();

    static void Main(string[] args)
    {
      Console.WriteLine("Welcome!\n");

      posts.AddRange(ProcessPosts().Result);

      var i = 0;
      foreach (var post in posts)
      {
        var output = $"[{i++}] {post.Name}\n";
        Console.WriteLine(output);
      }

      while (true)
      {
        int input;
        var result = Int32.TryParse(Console.ReadLine(), out input);
        if (result)
        {
          var markdown = ProcessSinglePost(posts[input].DownloadUrl).Result;
          Console.WriteLine(markdown);

        }
        else break;
      }
    }

    private static async Task<List<Post>> ProcessPosts()
    {
      client
        .DefaultRequestHeaders
        .Accept
        .Clear();

      var header = new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json");

      client
        .DefaultRequestHeaders
        .Accept
        .Add(header);

      client
        .DefaultRequestHeaders
        .Add("User-Agent", ".NET Foundation Repository Reporter");

      var streamTask = client.GetStreamAsync("https://api.github.com/repos/urizaraj/urizaraj.github.io/contents/_posts");

      var serializer = new DataContractJsonSerializer(typeof(List<Post>));

      var posts = serializer.ReadObject(await streamTask) as List<Post>;

      return posts;
    }

    private static async Task<string> ProcessSinglePost(Uri url)
    {
      var streamTask = client.GetStringAsync(url);

      return await streamTask;
    }
  }
}
