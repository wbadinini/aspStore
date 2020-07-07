using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AouraghStore;
using AouraghStore.Models;
using Microsoft.AspNet.Identity;
using UPFStore;
using UPFStore.Models;

namespace AouraghStore.Controllers
{
    [Authorize]
    public class OffersController : Controller
    {
        private UPFStoreModel db = new UPFStoreModel();

        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            var currentUserOffers = db.Offers
                .Where(o => o.CreatedBy == userId)
                .ToList();

            return View(currentUserOffers);
        }

        public ActionResult Details(long? id, string mode = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Include(o => o.OfferImages).FirstOrDefault(o => o.Id == id);
            if (offer == null)
            {
                return HttpNotFound();
            }

            ViewBag.readOnly = mode == "readOnly";
            return View(offer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Title,Description")] Offer offer, HttpPostedFileBase[] pictures)
        {
            if (ModelState.IsValid)
            {
                if (pictures != null)
                {
                    offer.OfferImages = new List<OfferImage>();

                    foreach (var picture in pictures)
                    {
                        if (!Directory.Exists(HttpContext.Server.MapPath("~/Images/")))
                        {
                            Directory.CreateDirectory(HttpContext.Server.MapPath("~/Images/"));
                        }

                        var fileName = $"{Guid.NewGuid()}.{picture.FileName.Split('.').Last()}";
                        picture.SaveAs(HttpContext.Server.MapPath("~/Images/") + fileName);

                        offer.OfferImages.Add(new OfferImage()
                        {
                            Path = fileName
                        });
                    }

                }

                offer.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offer);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View("Create", offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Title,Description")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offer);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
