using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Student_Courses
    {
        [ForeignKey("Students")]
        public int StudentID { get; set; }

        [ForeignKey("Courses")]
        public int CourseID { get; set; }
        public int ?Student_Grade { get; set; }
        public required Courses Courses { get; set; }
        public Students? Students { get; set; }
    }
    //For this class, the PK is a composite PK which CAN'T be done using Data annotations,so we must do it using fluent API (by overriding a Fn in Context file)
}
