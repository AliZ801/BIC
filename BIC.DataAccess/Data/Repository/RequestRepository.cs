using BIC.DataAccess.Data.Repository.IRepository;
using BIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIC.DataAccess.Data.Repository
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Request request)
        {
            var reqFromDb = _db.Requests.FirstOrDefault(r => r.Id == request.Id);

            reqFromDb.FirstName = request.FirstName;
            reqFromDb.LastName = request.LastName;
            reqFromDb.CompanyName = request.CompanyName;
            reqFromDb.Branch = request.Branch;
            reqFromDb.ServiceType = request.ServiceType;
            reqFromDb.Mobile = request.Mobile;
            reqFromDb.Email = request.Email;
            reqFromDb.City = request.City;
            reqFromDb.Address = request.Address;
            reqFromDb.Message = request.Message;
            reqFromDb.RequestDate = request.RequestDate;
            reqFromDb.Status = request.Status;

            _db.SaveChanges();
        }
    }
}
