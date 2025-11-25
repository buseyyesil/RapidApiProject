using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.RegisterDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateNewUserDto createNewUserDto)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form geçersiz!";
                return View();
            }

            var appUser = new AppUser()
            {
                Name = createNewUserDto.Name,
                Email = createNewUserDto.Mail,
                Surname = createNewUserDto.Surname,
                UserName = createNewUserDto.Username,
                WorkLocationID = 1,
                City = "Default",
                Country = "Turkey",
                Gender = "Erkek",
                ImageUrl = "/images/default-avatar.png",
                WorkDepartment = "1",
                EmailConfirmed = true
            };

           
            var result = await _userManager.CreateAsync(appUser, createNewUserDto.Password);

            if (result.Succeeded)
            {
                ViewBag.Success = "Kayıt başarılı!";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                
                ViewBag.Error = "Kayıt başarısız: ";
                foreach (var error in result.Errors)
                {
                    ViewBag.Error += error.Description + " ";
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}