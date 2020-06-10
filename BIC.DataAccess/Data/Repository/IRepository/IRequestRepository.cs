using BIC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIC.DataAccess.Data.Repository.IRepository
{
    public interface IRequestRepository : IRepository<Request>
    {
        void Update(Request request);
    }
}
