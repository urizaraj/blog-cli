using System;
using System.Runtime.Serialization;

namespace holding
{
  [DataContract(Name = "repo")]
  public class Repository
  {
    [DataMember(Name = "name")]
    public string Name { get; set; }
  }
}