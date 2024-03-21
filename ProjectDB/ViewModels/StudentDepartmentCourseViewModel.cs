using System.ComponentModel.DataAnnotations;

namespace ProjectDB.ViewModel
{
    public class StudentDepartmentCourseViewModel
    {
        public required string StudentName { get; set; }
        public int StudentId { get; set; }
        public required string DeptName { get; set; }
        public int DeptId { get; set; }
        public List<int> crsId { get; set; }
        public List<string>? courseName { get; set; }

        internal static object Select(Func<object, StudentDepartmentCourseViewModel> value)
        {
            throw new NotImplementedException();
        }
    }
}