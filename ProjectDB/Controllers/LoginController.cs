using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.LoginViewModel;
using ProjectDB.Models;
using System.Security.Claims;

namespace ProjectDB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult StdLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> StdLogin(StudentLogin model)
        { 
            ProjectDBContext dB = new ProjectDBContext();
           
            var res = dB.Student.FirstOrDefault(a => a.Student_Email == model.Student_Email);
        
            if (res == null)
            {
                ModelState.AddModelError("", "username and password invalid");
                return View(model);
            }
            //check in db
            Claim c1= new Claim(ClaimTypes.Name,res.Student_Name);
            Claim c2 = new Claim(ClaimTypes.Email, res.Student_Email);

            //
            ClaimsIdentity ci = new ClaimsIdentity("Cookies");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ClaimsPrincipal cp = new ClaimsPrincipal();
            cp.AddIdentity(ci);
            //
            await HttpContext.SignInAsync(cp);
            return RedirectToAction("ViewCoursesStudent", "Student", new { id = res.StudentID});

        }
        public IActionResult InsLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsLogin(InstructorLogin model)
        {

            ProjectDBContext dB = new ProjectDBContext();
            var res = dB.Instructors.FirstOrDefault(a => a.Ins_Email == model.Inst_Email);
            if (res == null)
            {
                ModelState.AddModelError("", "username and password invalid");
                return View(model);
            }
            //check in db
            Claim c1 = new Claim(ClaimTypes.Name, res.Ins_Name);
            Claim c2 = new Claim(ClaimTypes.Email, res.Ins_Email);
            //
            ClaimsIdentity ci = new ClaimsIdentity("Cookies");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ClaimsPrincipal cp = new ClaimsPrincipal();
            cp.AddIdentity(ci);
            //
            await HttpContext.SignInAsync(cp);
            return RedirectToAction("Index", "Instructor", new { id = res.InsID });

        }



    }
}
