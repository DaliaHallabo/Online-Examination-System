using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProjectDB.Models;
using ProjectDB.NewFolder;
using ProjectDB.Repository;

namespace ProjectDB.Controllers
{
    public class ExamController : Controller
    {
        ProjectDBContext db = new ProjectDBContext();

        public IActionResult GenerateExam(int id, int SI)
        {

            var questionsWithChoices = db.Questions
                .Where(q => q.CrsID == id && q.Question_Type == "MCQ")
                .Join(db.Choices, q => q.QuestionID, c => c.Q_ID, (q, c) => new { Question = q, Choice = c })
                .OrderBy(x => Guid.NewGuid()) 
                .Take(5) 
                .ToList();
            var questionsList = questionsWithChoices.Select(x => x.Question).ToList();
            //______________________

            List<Questions> tfQuestions = db.Questions.Where(q => q.CrsID == id && q.Question_Type == "True/False").ToList();

            // Shuffle the T/F questions
            tfQuestions = tfQuestions.OrderBy(q => Guid.NewGuid()).Take(5).ToList();

            // Combine MCQ and T/F questions
            List<Questions> selectedQuestions = questionsList.Concat(tfQuestions).ToList();

            // Separate the questions and choices into two lists   
            var choicesList = questionsWithChoices.Select(x => x.Choice).ToList();
            ViewBag.ListChoices= choicesList;
            ViewBag.StdId = SI;

            if (questionsList.Count < 5)
            {
                return View("Error");
            }
 
            return View(selectedQuestions);
        }

        [HttpPost]
        public IActionResult ProcessExam(List<int> questionIds, List<string> studentResponses, int SI,int id/*, string EI*/)
        {
            int grade = 0;

           
            List<string> RightAnswer = new List<string>();

            for (var i = 0; i < questionIds.Count(); i++)
            {
                var answer = db.Questions
                       .Where(q => q.QuestionID == questionIds[i])
                       .Select(a => a.Question_Answer)
                       .FirstOrDefault();
                if(answer!=null)
                {
                    RightAnswer.Add(answer);
                }
            }
         

            var crsWorkGrade = int.Parse(db.Course
           .Where(c => c.CourseID == id)
           .Join(db.Student_Courses, c => c.CourseID, s => s.CourseID, (c, s) => new { Courses = c, Student_Courses = s })
           .Select(c => c.Courses.CourseWork).FirstOrDefault()!);

            int TotalMarks = 100 - crsWorkGrade;

            for (int i =0;i<studentResponses.Count();i++)
            {
                if (studentResponses[i] == RightAnswer[i])
                {
                    /*int cWork*/
                    grade += TotalMarks/10;
                }
            }

            Exams Exam = new Exams {Total_Marks = TotalMarks, CrsID = id };
            db.Exams.Add(Exam);
            db.SaveChanges();

            // Retrieve the generated PK value
            int generatedPKValue = Exam.ExamID;

      
            var studentCourse = db.Student_Courses
                .FirstOrDefault(c => c.CourseID == id && c.StudentID == SI);

            if (studentCourse != null)
            {
                studentCourse.Student_Grade = grade;
                db.SaveChanges(); // Commit changes to the database
            }
            ViewBag.totalMarks = TotalMarks;
            ViewBag.StdId = SI;

            for (var i = 0; i < questionIds.Count(); i++)
            {
                ExamQuestionResponse stdRes = new ExamQuestionResponse()
                { ExamID = generatedPKValue, StudentID = SI, QuestionID = questionIds[i], StudentResponse = studentResponses[i] };

       
                db.examQuestionResponses.Add(stdRes);
                db.SaveChanges();
            }

            return View(grade);

        }


    }
}

