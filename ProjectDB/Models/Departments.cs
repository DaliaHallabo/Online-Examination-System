using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectDB.Models
{
    public class Departments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptID { get; set; } 
        public string DeptName { get; set; }
        public List<ITIBranches> ITIBranches { get; set; }
        public List<Instructors> Instructors { get; set; }
    }
}
