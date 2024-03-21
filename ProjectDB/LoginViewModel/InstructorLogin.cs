using System.ComponentModel.DataAnnotations;

namespace ProjectDB.LoginViewModel
{
    public class InstructorLogin
    {
        [Required]
        [RegularExpression(@"[a-z A-Z 0-9 _-_.]+@[a-z A-Z]+.[a-z  A-Z]{2,4}")]
        public string Inst_Email { get; set; }
        [DataType(DataType.Password)]

        public string Ins_Pass { get; set; }
    }
}
