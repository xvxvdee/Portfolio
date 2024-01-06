using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiService.Services;

namespace homeController.Controllers;

[Route("Deandra")]

public class HomeController : Controller{
    private readonly ApiService apiService = new ApiService();

    [HttpGet("about")]
    public async Task<IActionResult> About(){
        ViewData["Title"] = "About";
        ViewData["Description"] = await this.apiService.GetAbout();
        return View(); // Looks for Views/Home/About.cshtml
    }
}