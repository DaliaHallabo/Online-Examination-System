using ProjectDB.Models;

namespace ProjectDB.Repository
{
    public interface IExamRepo
    {
        List<Exams> GetAll();
        void Add(Exams d);
        void Update(Exams d);
        
    }
    public class ExamRepo: IExamRepo
    {
       
        
            ProjectDBContext db = new ProjectDBContext();
            public List<Exams> GetAll()
            {
              return db.Exams.ToList();
           }
     
            public void Add(Exams Exam)
            {
                db.Exams.Add(Exam);
                db.SaveChanges();

            }
            public void Update(Exams Exam)
            {
                db.Exams.Update(Exam);
                db.SaveChanges();
            }
            
        
    }
}
