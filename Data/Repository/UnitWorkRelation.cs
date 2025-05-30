using Data.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitWorkRelation : IUnitWorkRelation
    {
        private readonly ApplicationDbContext _db;
        public IRelationRepository SubjectUser { get; private set; }

        public UnitWorkRelation(ApplicationDbContext db)
        {
            _db = db;
            SubjectUser = new SubjectUserRepository(db);
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
            int count = _db.Relations.Count();
            return count;
        }
    }
}
