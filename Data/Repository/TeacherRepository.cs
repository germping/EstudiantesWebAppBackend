using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TeacherRepository : Repository<User>, ITeacherRepository
    {
        private readonly ApplicationDbContext _db;

        public TeacherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            var userDb = _db.Users.FirstOrDefault(e => e.Id == user.Id);
            if (userDb != null)
            {
                userDb.Username = user.Username;
                userDb.Email = user.Email;
                _db.SaveChanges();
                
            }
        }
    }
}
