using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Course_Topics
    {
        [ForeignKey("Courses")]
        public int CourseID { get; set; }
        public Courses Courses { get; set; }
        public string Topics { get; set; }
    }
}
