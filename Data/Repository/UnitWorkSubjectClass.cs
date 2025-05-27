using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitWorkSubjectClass : IUnitWorkSubjectClass
    {
        private readonly ApplicationDbContext _db;
        public IClassSubjectRepository ClassSubject { get; private set; }

        public UnitWorkSubjectClass(ApplicationDbContext db)
        {
            _db = db;
            ClassSubject= new ClassSubjectRepository(db);
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
