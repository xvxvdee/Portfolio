using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace EducationModel.Models;
public class Education {
    public int? Id {get; set;}
    public string? School {get; set;}
    public string? Program {get; set;}
    public string? Degree {get; set;}
    [DataType(DataType.Date)]
    public DateTime? StartDate {get; set;}
    [DataType(DataType.Date)]
    public DateTime? EndDate {get; set;}
    public List<BsonString>? Extracurriculars {get; set;}
}