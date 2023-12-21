using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ProjectModel.Models;
public class Project {
    public int? Id {get; set;}
    public string? Title {get; set;}
    public string? Description {get; set;}
    public string? Repository {get; set;}
    public bool Collaboration {get; set;}
    public List<string>? TechStack  {get; set;}
}