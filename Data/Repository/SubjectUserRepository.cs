using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SubjectUserRepository : Repository<SubjectUser>, IRelationRepository
    {
        private readonly ApplicationDbContext _db;

        public SubjectUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SubjectUser subjectUser)
        {
            var subjectUserDb = _db.Relations.FirstOrDefault(e => e.Id == subjectUser.Id);
            if (subjectUserDb != null)
            {
                subjectUserDb.UserId = subjectUser.UserId;
                subjectUserDb.ClassSubjectId = subjectUser.ClassSubjectId;
                _db.SaveChanges();
                
            }
        }
    }
}
