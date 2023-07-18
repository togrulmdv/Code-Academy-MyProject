using Microsoft.AspNetCore.Mvc;
using MyProject.Models;

namespace MyProject.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        List<Student> students = new List<Student>()
            {
                new Student() { Id = 1, Name = "Togrul", Surname = "Mammadov", Point = 94},
                new Student() { Id = 2, Name = "Shahmar", Surname = "Huseynov", Point = 89},
                new Student() {Id = 3, Name = "Ilham", Surname = "Ganiyev", Point = 72}
            };

        return View(students);
    }
}
