using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Exams
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }
        public int Total_Marks { get; set; } //Total_Marks is the maximum mark for exam
        [ForeignKey("Courses")]
        public int CrsID { get; set; }
        public Courses Courses { get; set; }
    }
}
