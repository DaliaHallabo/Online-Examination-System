using Microsoft.EntityFrameworkCore;

namespace ProjectDB.Models
{
    public class ProjectDBContext: DbContext //Creating the database
    {
        public DbSet<Students> Student { get; set; } 
        public DbSet<Departments> Department { get; set; }
        public DbSet<Courses> Course { get; set; }
        public DbSet<ITIBranches> ITIBranches { get; set;}
        public DbSet<Student_Courses> Student_Courses { get; set; } //PK identified here using Fluent API
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<ExamQuestionResponse> examQuestionResponses { get; set; }

        public DbSet<Choices> Choices { get; set; }  //PK identified here using Fluent API
        public DbSet<Course_Topics> Course_Topics { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = .; Initial Catalog = Project_MVC_17_3; Integrated Security = True; Trust Server Certificate = True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Courses>().HasKey(c => new { c.StudentID, c.CourseID });    //Name of Properties in Student_Courses model.
            modelBuilder.Entity<Course_Topics>().HasKey(c => new { c.CourseID, c.Topics });         //Name of Properties in Course_Topics model (multi-valued table)
            modelBuilder.Entity<ExamQuestionResponse>().HasKey(c => new { c.StudentID, c.QuestionID, c.ExamID });  //Name of Properties in ExamQuestionResponse model (Ternary Relationship)
            base.OnModelCreating(modelBuilder);
        }
    }
}
