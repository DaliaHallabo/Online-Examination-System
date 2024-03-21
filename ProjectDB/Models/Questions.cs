using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Questions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        public required string Question_Text { get; set; }
        public required string Question_Type { get; set; }
        public required string Question_Answer { get; set; }

        [ForeignKey("Courses")]
        public int CrsID { get; set; }
        public Courses? Courses { get; set; }
        public Choices Choices { get; set; }
    }
}
