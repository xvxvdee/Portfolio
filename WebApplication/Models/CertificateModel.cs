using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace CertificateModel.Models;
public class Certificate {
    public string? Title {get; set;}
    public string? Issuer {get; set;}
    [DataType(DataType.Date)]
    public DateTime DateIssued {get; set;}
    public string? CredentialID {get; set;}
}