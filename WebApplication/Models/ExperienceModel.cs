using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace ExperienceModel.Models;
public class Experience {
    public string? Position {get; set;}
    public string? Company {get; set;}
    public string? Location {get; set;}
    public string? Type {get; set;}
    public string? Description {get; set;}
    [DataType(DataType.Date)]
    public DateTime? StartDate {get; set;}
    [DataType(DataType.Date)]
    public DateTime? EndDate {get; set;}
    public List<string>? SkillsDeveloped  {get; set;}
}