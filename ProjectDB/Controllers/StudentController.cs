using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDB.LoginViewModel;
using ProjectDB.Models;
using ProjectDB.NewFolder;
using ProjectDB.Repository;
using ProjectDB.ViewModel;
using ProjectDB.ViewModels;

namespace ProjectDB.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepo studentRepo;
        ProjectDBContext db = new ProjectDBContext();
        public StudentController(IStudentRepo _studentRepo)
        {
            studentRepo = _studentRepo;
        }


        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult ViewCoursesStudent(int id)
        {
            
            if (id == 0)
            {
                return BadRequest();
            }
            var stdmodel = db.Student.Include(m => m.departments).FirstOrDefault(a => a.StudentID == id);
            var stdcrsIdmodel = db.Student_Courses
                                .Include(sc => sc.Courses)
                                .Where(sc => sc.StudentID == id)
                                .Select(sc => sc.Courses.CourseID)
                                .ToList();
            if (stdmodel == null)
            {
                return NotFound();
            }

            var stdcrsmodel = db.Student_Courses
                                .Include(sc => sc.Courses)
                                .Where(sc => sc.StudentID ==id)
                                .Select(sc => sc.Courses.Course_Name)
                                .ToList();
 
            if (stdcrsmodel == null)
            {
                return NotFound();
            }


            // Create a ViewModel for each course
            var model = new StudentDepartmentCourseViewModel
            {
                StudentId = stdmodel.StudentID,
                StudentName = stdmodel.Student_Name,
                DeptName = stdmodel.departments!.DeptName, // Use null-conditional operator
                DeptId = stdmodel.Dept_ID,
                courseName = stdcrsmodel, // Directly access course name
                crsId = stdcrsIdmodel,
            };
            return View(model);
        }
        public IActionResult ShowGrade(string courseIds, int SI)
        {
            // Split the comma-separated string of course IDs into an array
            var courseIdArray = courseIds.Split(',').Select(int.Parse).ToList();
            List<int> crswrklist = new List<int>();

            var model = db.Student_Courses.Include(sc => sc.Courses).Include(s => s.Students)
                             .Where(sc => sc.StudentID == SI).ToList();

            foreach (var crs in courseIdArray)
            {
                var crsWorkGrade = int.Parse(db.Course
                 .Where(c => c.CourseID ==crs )
                 .Join(db.Student_Courses, c => c.CourseID, s => s.CourseID, (c, s) => new { Courses = c, Student_Courses = s })
                  .Select(c => c.Courses.CourseWork).FirstOrDefault()!);
                   crswrklist.Add(crsWorkGrade);
            }
            ViewBag.courseWork=crswrklist;
           
            return View(model);
        }

    }
}
