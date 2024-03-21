using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ProjectDB.Models
{
    public class Instructors
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsID { get; set; }
        public string Ins_Name { get; set; }

        [Required]
        [RegularExpression(@"[a-z A-Z 0-9 _-]+@[a-z A-Z]+.[a-z  A-Z]{2,4}")]
        public string Ins_Email {  get; set; }
        public List<Departments> Departments { get; set; }
    }
}
