using System;
using System.Runtime.Serialization;

namespace holding
{
  [DataContract(Name = "post")]
  public class Post
  {
    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "path")]
    public string Path { get; set; }

    [DataMember(Name = "html_url")]
    public Uri HtmlUrl { get; set; }

    [DataMember(Name = "download_url")]
    public Uri DownloadUrl { get; set; }
  }
}