using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BIC.Models;
using BIC.DataAccess.Data.Repository.IRepository;
using BIC.Models.ViewModel;
using BIC.Utility;

namespace BIC.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public RequestVM requestVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET ServReq Action Method
        public IActionResult ServReq()
        {
            requestVM = new RequestVM() { Request = new Models.Request() };

            return View(requestVM);
        }

        //POST ServReq Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ServReq")]
        public IActionResult ServReqPOST()
        {
            if (ModelState.IsValid)
            {
                requestVM.Request.RequestDate = DateTime.Now;
                requestVM.Request.Status = SD.Submitted;

                _unitofWork.Request.Add(requestVM.Request);

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(requestVM);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
