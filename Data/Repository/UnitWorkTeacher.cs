using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitWorkTeacher : IUnitWorkTeacher
    {
        private readonly ApplicationDbContext _db;
        public ITeacherRepository User { get; private set; }

        public UnitWorkTeacher(ApplicationDbContext db)
        {
            _db = db;
            User= new TeacherRepository(db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public int GetCountRegisters()
        {
            int count=_db.Classes.Count();
            return count;
        }
    }
}
