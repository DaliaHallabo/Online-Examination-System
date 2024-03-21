using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDB.Models
{
    public class ITIBranches
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchID { get; set; }
        public string Branch_Name { get; set;}
        public List<Departments> Departments { get; set; }
    }
}
