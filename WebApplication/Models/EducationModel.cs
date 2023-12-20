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
    public BsonInt32? Id {get; set;}
    public BsonString? School {get; set;}
    public BsonString? Program {get; set;}
    public BsonString? Degree {get; set;}
    [DataType(DataType.Date)]
    public BsonDateTime? StartDate {get; set;}
    [DataType(DataType.Date)]
    public BsonDateTime? EndDate {get; set;}
    public List<BsonString>? Extracurriculars {get; set;}
}