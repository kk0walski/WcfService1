using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WcfClient.ServiceReference1;

namespace WcfClient.Controllers
{
    public class AuctionsController : Controller
    {
        // GET: Auctions
        public ActionResult Index()
        {
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            return View(obj.getAll());
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            return View(obj.getById(id.ToString()));
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Finished,Name,StartPrice,EndDate")]Auction auction)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                obj.AddAutcion(auction);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auctions/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            Auction aukcja = obj.getById(id.ToString());
            if (aukcja == null)
            {
                return HttpNotFound();
            }
            return View(aukcja);
        }

        // POST: Auctions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "ID,Finished,Name,StartPrice,EndDate")]Auction auction)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                obj.UpdateAuction(auction, id.ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auctions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            Auction aukcja = obj.getById(id.ToString());
            if (aukcja == null)
            {
                return HttpNotFound();
            }
            return View(aukcja);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                obj.deleteAuction(id.ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Auctions/id/Offers
        public ActionResult OffersIndex(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AuctionId = id;
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            return View(obj.getOffers(id.ToString()));
        }

        // GET: Auctions/5/Offers/Details/3
        public ActionResult DetailsOffer(int id, int offerId)
        {
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            return View(obj.getOfferById(id.ToString(), offerId.ToString()));
        }

        // GET: Auctions/id/OffersCreate
        public ActionResult CreateOffer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        // POST: Auctions/id/Offers/Create
        [HttpPost]
        public ActionResult CreateOffer(int id, [Bind(Include = "offerId,auction,price,date")]Offer offer)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                Auction aukcja = obj.getById(id.ToString());
                if (offer != null && aukcja.EndDate > offer.Date && !aukcja.Finished && aukcja.EndDate > DateTime.Now && offer.Date <= DateTime.Now)
                {
                    obj.addOffer(offer, id.ToString());
                    return RedirectToAction("OffersIndex");
                }
                else
                {
                    ModelState.AddModelError("Offer", "Bad offer informations");
                    return View(offer);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Auctions/Edit/5
        public ActionResult OfferEdit(int? id, int? offerId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            Auction aukcja = obj.getById(id.ToString());
            if (aukcja == null)
            {
                return HttpNotFound();
            }
            Offer oferta = obj.getOfferById(id.ToString(), offerId.ToString());
            if(oferta == null)
            {
                return HttpNotFound();
            }
            return View(aukcja);
        }

        // POST: Auctions/5/Offers/5
        [HttpPost]
        public ActionResult OfferEdit(int id, int offerId, [Bind(Include = "offerId,auction,price,date")]Offer offer)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                Auction aukcja = obj.getById(id.ToString());
                if (offer != null && aukcja.EndDate > offer.Date && !aukcja.Finished && aukcja.EndDate > DateTime.Now && offer.Date <= DateTime.Now)
                {
                    obj.UpdateOffer(offerId.ToString(), id.ToString(), offer);
                    return RedirectToAction("OffersIndex");
                }
                else
                {
                    ModelState.AddModelError("Offer", "Wrong date or null ofer");
                    return View(offer);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteOffer(int? id, int? offerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            Auction aukcja = obj.getById(id.ToString());
            if (aukcja == null)
            {
                return HttpNotFound();
            }
            Offer oferta = obj.getOfferById(id.ToString(), offerId.ToString());
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(aukcja);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOfferConfirmed(int id, int idOffer)
        {
            try
            {
                ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
                Auction aukcja = obj.getById(id.ToString());
                Offer oferta = obj.getOfferById(id.ToString(), idOffer.ToString());
                if (oferta != null && aukcja.EndDate > oferta.Date && !aukcja.Finished)
                {
                    obj.deleteOffer(id.ToString(), idOffer.ToString());
                    return RedirectToAction("OffersIndex");
                }
                else
                {
                    ModelState.AddModelError("Offer", "you are trying to delete offer after auction finsished");
                    return View(oferta);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
