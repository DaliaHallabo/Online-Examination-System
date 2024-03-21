using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Courses
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string Course_Name { get; set; }
        public string CourseWork { get; set; }

        [ForeignKey("Instructors")]
        public int Ins_ID {  get; set; }
        public Instructors Instructors { get; set; }

    }
}
