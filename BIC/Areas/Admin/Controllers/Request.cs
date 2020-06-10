using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIC.DataAccess.Data.Repository.IRepository;
using BIC.Models.ViewModel;
using BIC.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BIC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Request : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public RequestVM requestVM { get; set; }

        public Request(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            return View(reqFromDb);
        }

        public IActionResult Approved(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            reqFromDb.Status = SD.Pending;

            _unitofWork.Request.Update(reqFromDb);
            _unitofWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Completed(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            reqFromDb.Status = SD.WorkCompleted;

            _unitofWork.Request.Update(reqFromDb);
            _unitofWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult InvoiceSub(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            reqFromDb.Status = SD.InvoiceSubmitted;

            _unitofWork.Request.Update(reqFromDb);
            _unitofWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Payment(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            reqFromDb.Status = SD.PaymentRecieved;

            _unitofWork.Request.Update(reqFromDb);
            _unitofWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        public IActionResult GetAllRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll() });
        }

        public IActionResult GetAllSubmittedRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.Submitted) });
        }

        public IActionResult GetAllApprovedRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.Approved) });
        }

        public IActionResult GetAllPendingRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.Pending) });
        }

        public IActionResult GetAllCompletedRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.WorkCompleted) });
        }

        public IActionResult GetAllInvoiceSubmittedRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.InvoiceSubmitted) });
        }

        public IActionResult GetAllPaymentRequest()
        {
            return Json(new { data = _unitofWork.Request.GetAll(filter: r => r.Status == SD.PaymentRecieved) });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var reqFromDb = _unitofWork.Request.Get(id.GetValueOrDefault());

            if(reqFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting!" });
            }

            _unitofWork.Request.Remove(reqFromDb);

            _unitofWork.Save();

            return Json(new { success = true, message = "Request Deleted Succesfully!" });
        }

        #endregion
    }
}
