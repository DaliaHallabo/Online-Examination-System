using Microsoft.EntityFrameworkCore;
using ProjectDB.Models;
namespace ProjectDB.Repository
{
    public class InstructorRepo
    {
        ProjectDBContext db = new ProjectDBContext();
        //-------------------------------------------------------------------------------------------------
        public Instructors GetById(int id)
        {
            return db.Instructors.Include(a => a.Departments).FirstOrDefault(a => a.InsID == id);
        }
        //-------------------------------------------------------------------------------------------------
        public List<Students> StudentsByDeptID(int id)
        {
            return db.Student.Where(a => a.Dept_ID == id).ToList();
        }
        //-------------------------------------------------------------------------------------------------
        public int GetInstructorIdByStudentId(int studentId)
        {
            // Find the courses in which the student is enrolled
            var studentCourses = db.Student_Courses.Where(sc => sc.StudentID == studentId).Include(sc => sc.Courses).ToList();

            // Retrieve the instructor ID for the first course
            var firstCourse = studentCourses.FirstOrDefault()?.Courses;

            if (firstCourse != null) { return firstCourse.Ins_ID; }
            return 0;
        }
        //-------------------------------------------------------------------------------------------------

        public int GetDepartmentIdByStudentId(int studentId)
        {
            var student = db.Student.FirstOrDefault(s => s.StudentID == studentId);
            if (student != null) { return student.Dept_ID; }
            return 0;
        }
        //-------------------------------------------------------------------------------------------------
        public int GetInstructorIdByDepartmentId(int departmentId)
        {
            var instructor = db.Instructors.FirstOrDefault(i => i.Departments.Any(d => d.DeptID == departmentId));
            if (instructor != null) { return instructor.InsID; }
            return 0;
        }
        //-------------------------------------------------------------------------------------------------
        public List<Student_Courses> GetStudentIDandCourseID(int id)    //We need a ViewBag for students grades since it's in exams table
        {
            return db.Student_Courses.Where(a => a.StudentID == id).ToList();
        }
        //-------------------------------------------------------------------------------------------------
        public List<Courses> GetAllCoursesForEachInstructor(int id)
        {
            var courses = db.Course.Where(a => a.Ins_ID == id).ToList();
            return courses;
        }
        //-------------------------------------------------------------------------------------------------
        public List<Questions> DisplayQuestions(int id)
        {
            return db.Questions.Where(a => a.CrsID == id).ToList();
        }
        //-------------------------------------------------------------------------------------------------
        public int StudentsCount(int id)
        {
            return db.Student_Courses.Where(a => a.CourseID == id).Count();
        }
        //-------------------------------------------------------------------------------------------------
        public List<Course_Topics> DisplayCourseTopics(int id)
        {
            return db.Course_Topics.Where(a => a.CourseID == id).ToList();
        }
        //-------------------------------------------------------------------------------------------------
        public bool ExamIdExists(int ExamId)
        {

            return db.examQuestionResponses.Any(a => a.ExamID == ExamId);
        }
        //-------------------------------------------------------------------------------------------------
        public bool StudentIdExists(int StdID)
        {
            return db.Student.Any(a => a.StudentID == StdID);
        }
        //-------------------------------------------------------------------------------------------------
        public Exams GetExamById(int examId)
        {
            return db.Exams.FirstOrDefault(e => e.ExamID == examId);
        }
        //-------------------------------------------------------------------------------------------------
        public List<ExamQuestionResponse> GetQuestionsByExamId(int ExamId)
        {
            return db.examQuestionResponses.Include(e => e.Questions).ThenInclude(q => q.Choices).Where(a => a.ExamID == ExamId).ToList();
        }
        //-------------------------------------------------------------------------------------------------
        public List<ExamQuestionResponse> GetQuestionsAndStudentResponseByExamAndStudentID(int ExamId, int StdID)
        {
            return db.examQuestionResponses.Include(e => e.Questions).Where(a => a.ExamID == ExamId && a.StudentID == StdID).ToList();
        }
        //-------------------------------------------------------------------------------------------------


    }
}
