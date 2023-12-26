using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace CourseModel.Models;
public class Course {
    public int? Id {get; set;}
    public string? Title {get; set;}
    public string? Institution {get; set;}
    public string? CourseCode {get; set;}
    public string? CourseWork {get; set;}
}