using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPFStore.Controllers
{
    public class HomeController : Controller
    {
        private UPFStoreModel db = new UPFStoreModel();

        public ActionResult Index()
        {
            return View(db.Offers.ToList());
        }
    }
}