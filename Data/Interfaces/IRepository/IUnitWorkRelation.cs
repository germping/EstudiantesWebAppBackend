using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface IUnitWorkRelation : IDisposable
    {
        IRelationRepository SubjectUser { get; }
        Task Save();
        int GetCountRegisters();
    }

}
