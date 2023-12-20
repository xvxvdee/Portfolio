using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace VolunteerModel.Models;
public class Volunteer {    
    public string? Position {get; set;}
    public string? Company {get; set;}
    public string? Field {get; set;}
    public string? Description {get; set;}
    [DataType(DataType.Date)]
    public DateTime? StartDate {get; set;}
    [DataType(DataType.Date)]
    public DateTime? EndDate {get; set;}
    public List<string>? Responsibilities {get; set;}
}