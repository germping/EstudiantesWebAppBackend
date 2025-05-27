﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface IUnitWorkSubjectClass: IDisposable
    {
        IClassSubjectRepository ClassSubject { get; }
        Task Save();
        int GetCountRegisters();
    }
}
