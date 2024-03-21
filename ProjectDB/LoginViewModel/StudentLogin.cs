using System.ComponentModel.DataAnnotations;

namespace ProjectDB.LoginViewModel
{
    public class StudentLogin
    {
        [Required]
        [RegularExpression(@"[a-z A-Z 0-9 _-]+@[a-z A-Z]+.[a-z  A-Z]{2,4}")]
        public string Student_Email { get; set; }
        [DataType(DataType.Password)]
        public string Student_Pass { get; set; }   
    }
}
