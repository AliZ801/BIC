using System;
using System.Collections.Generic;
using System.Text;

namespace BIC.DataAccess.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IRequestRepository Request { get; }

        void Save();
    }
}
