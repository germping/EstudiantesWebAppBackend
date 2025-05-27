using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ClassSubjectRepository : Repository<ClassSubject>, IClassSubjectRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassSubjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassSubject classSubject)
        {
            var classSubjectDb = _db.Classes.FirstOrDefault(e => e.Id == classSubject.Id);
            if (classSubjectDb != null)
            {
                classSubjectDb.Title = classSubject.Title;
                classSubjectDb.Credits = classSubject.Credits;
                _db.SaveChanges();
                
            }
        }
    }
}
