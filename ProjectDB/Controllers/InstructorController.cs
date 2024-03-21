using ProjectDB.Models;
using ProjectDB.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ProjectDB.Controllers
{
    public class InstructorController : Controller
    {
        InstructorRepo InstructorRepo = new InstructorRepo();
        //--------------------------------------------------------------------------------
        public IActionResult Index(int? id)      //This should be passed from the action where the Instructor logged in to instead
        {
            if (id == null) { return BadRequest(); }

            var model = InstructorRepo.GetById(id.Value);
            if (model == null) { return NotFound(); }

            var course = InstructorRepo.GetAllCoursesForEachInstructor(id.Value).FirstOrDefault();
            ViewBag.Course = course;

            return View(model);
        }
        //--------------------------------------------------------------------------------
        public IActionResult DisplayStudentsByDepartmentID(int? id)
        {
            if (id == null) { return BadRequest(); }

            var mdl = InstructorRepo.StudentsByDeptID(id.Value);
            if (mdl == null) { return NotFound(); };

            int instructorId = InstructorRepo.GetInstructorIdByDepartmentId(id.Value);
            // Set the instructor ID in ViewBag for the GoBack button
            ViewBag.InstructorId = instructorId;

            return View(mdl);
        }
        //--------------------------------------------------------------------------------
        public IActionResult DisplayEachStudentGradesInAllCourses(int? id)
        {
            if (id == null) { return BadRequest(); }

            var studentCoursesWithGrades = InstructorRepo.GetStudentIDandCourseID(id.Value);

            if (studentCoursesWithGrades == null) { return NotFound(); }

            // Retrieve instructor ID based on student ID
            var instructorId = InstructorRepo.GetInstructorIdByStudentId(id.Value);

            // Set the instructor ID in ViewBag for the GoBack button
            ViewBag.InstructorId = instructorId;


            return View(studentCoursesWithGrades);
        }
        //--------------------------------------------------------------------------------

        public IActionResult DisplayCoursesForInstructor(int? id)
        {
            if (id == null) { return BadRequest(); }

            var model = InstructorRepo.GetAllCoursesForEachInstructor(id.Value);
            if (model == null) { return NotFound(); };

            var model2 = InstructorRepo.StudentsCount(id.Value);
            ViewBag.Student_Courses = model2;

            // Set the instructor ID in ViewBag for the GoBack button
            ViewBag.InstructorId = id;

            return View(model);
        }
        //--------------------------------------------------------------------------------
        public IActionResult DisplayCourseQuestions(int? id)
        {
            if (id == null) { return BadRequest(); }
            var model = InstructorRepo.DisplayQuestions(id.Value);

            if (model == null) { return NotFound(); };

            return View(model);
        }
        //--------------------------------------------------------------------------------
        public IActionResult DisplayTopics(int? id)
        {
            if (id == null) { return BadRequest(); }

            var model = InstructorRepo.DisplayCourseTopics(id.Value);
            if (model == null) { return NotFound(); };

            return View(model);
        }
        //--------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult DisplayExamQuestions(int ExamId, int id)
        {
            if (string.IsNullOrWhiteSpace(ExamId.ToString()) || !int.TryParse(ExamId.ToString(), out int parsedExamId) || parsedExamId < 1)
            {
                return PartialView("MyPartialView", "Please enter a valid value for the Exam ID.");
            }

            if (!InstructorRepo.ExamIdExists(ExamId))
            {
                return PartialView("MyPartialView", "No exam found with the provided ID.");
            }

            var instructorCourses = InstructorRepo.GetAllCoursesForEachInstructor(id);
            var exam = InstructorRepo.GetExamById(ExamId);

            if (!instructorCourses.Any(c => c.CourseID == exam.CrsID))
            {
                return PartialView("MyPartialView", "You are not authorized to view this exam.");
            }

            // Set the instructor ID in ViewBag for the GoBack button
            ViewBag.InstructorId = id;

            var model = InstructorRepo.GetQuestionsByExamId(ExamId);

            return View("DisplayExamQuestions", model);
        }
        //--------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult DisplayResponses(int ExamId, int StdID, int id)
        {
            int parsedExamId, parsedStdID; 

            if ((string.IsNullOrWhiteSpace(ExamId.ToString()) || !int.TryParse(ExamId.ToString(), out parsedExamId) || parsedExamId < 1) && (string.IsNullOrWhiteSpace(StdID.ToString()) || !int.TryParse(StdID.ToString(), out parsedStdID) || parsedStdID < 1))
            {
                return PartialView("MyPartialView2", "Please enter valid values for both the Exam ID and Student ID.");
            }

            if (string.IsNullOrWhiteSpace(ExamId.ToString()) || !int.TryParse(ExamId.ToString(), out parsedExamId) || parsedExamId < 1)
            {
                return PartialView("MyPartialView2", "Please enter a valid value for the Exam ID.");
            }

            if (string.IsNullOrWhiteSpace(StdID.ToString()) || !int.TryParse(StdID.ToString(), out parsedStdID) || parsedStdID < 1)
            {
                return PartialView("MyPartialView2", "Please enter a valid value for the Student ID.");
            }

            if (!InstructorRepo.ExamIdExists(ExamId) && !InstructorRepo.StudentIdExists(StdID))
            {
                return PartialView("MyPartialView2", "Neither Exam ID or Student ID found with the provided IDs.");
            }

            if (!InstructorRepo.ExamIdExists(ExamId))
            {
                return PartialView("MyPartialView2", "No exam found with the provided ID.");
            }

            if (!InstructorRepo.StudentIdExists(StdID))
            {
                return PartialView("MyPartialView2", "No student found with the provided ID.");
            }

            var instructorCourses = InstructorRepo.GetAllCoursesForEachInstructor(id);
            var exam = InstructorRepo.GetExamById(ExamId);

            if (!instructorCourses.Any(c => c.CourseID == exam.CrsID))
            {
                return PartialView("MyPartialView2", "You are not authorized to view this exam.");
            }

            // Set the instructor ID in ViewBag for the GoBack button
            ViewBag.InstructorId = id;

            var model = InstructorRepo.GetQuestionsAndStudentResponseByExamAndStudentID(ExamId, StdID);
            return View("DisplayResponses", model);
        }
        //--------------------------------------------------------------------------------
    }
}
