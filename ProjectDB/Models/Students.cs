using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
namespace ProjectDB.Models
{
    public class Students
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "The name field is required")]
        [StringLength(20, ErrorMessage = "Max chars are 20")]
        [Display(Name = "Full Name")]
        public string Student_Name { get; set;}

        [Required]
        [RegularExpression(@"[a-z A-Z 0-9 _-]+@[a-z A-Z]+.[a-z  A-Z]{2,4}")]
        public string Student_Email { get; set;}

        [DataType(DataType.Date)]
        public DateTime Student_BD {  get; set;} 

        [ForeignKey("departments")]
        public int Dept_ID { get; set; }
        public Departments departments { get; set; }
    }
}
