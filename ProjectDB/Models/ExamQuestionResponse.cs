using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class ExamQuestionResponse
    {
        [ForeignKey("Students")]
        public int StudentID { get; set; }

        [ForeignKey("Exams")]
        public int ExamID { get; set; }

        [ForeignKey("Questions")]
        public int QuestionID { get; set; }

        public string? StudentResponse { get; set; }

        public Exams Exams { get; set; }
        public Questions Questions { get; set; }
        public Students Students { get; set; }
    }
}
