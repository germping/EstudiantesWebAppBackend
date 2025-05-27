using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface IUnitWorkTeacher: IDisposable
    {
        ITeacherRepository User { get; }
        Task Save();
        int GetCountRegisters();
    }
}
