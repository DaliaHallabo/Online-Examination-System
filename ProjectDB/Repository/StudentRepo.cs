using ProjectDB.Models;
using Microsoft.EntityFrameworkCore;
namespace ProjectDB.NewFolder
{
    public interface IStudentRepo
    {
        List<Students> GetAll();
        Students GetById(int id);
        void Add(Students student);
        void Update(Students student);
        void Delete(int id);
    }


    public class StudentRepo:IStudentRepo
    {
        ProjectDBContext db = new ProjectDBContext();
        public List<Students> GetAll()
        {
            return db.Student.Include(s=>s.departments).ToList();
            
        }
        public Students GetById(int id)
        {
            return db.Student.Include(s => s.departments).SingleOrDefault(s => s.Dept_ID == id);
        }
        public void Add(Students student)
        {
            db.Student.Add(student);
            db.SaveChanges();
            //return student.Id;
        }
        public void Update(Students student)
        {
            db.Student.Update(student);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var s = db.Student.FirstOrDefault(a => a.Dept_ID == id);
            db.Student.Remove(s);
            db.SaveChanges();
        }
    }
}
