using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bai5.Models;
using PagedList;
using PagedList.Mvc;

namespace Bai5.Controllers
{
    public class VoucherController : Controller
    {
        Database1Entities database = new Database1Entities();

        // GET: Voucher
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(database.Vouchers.ToList().OrderBy(n => n.VoucherID).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                database.Vouchers.Add(voucher);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Voucher voucher = database.Vouchers.SingleOrDefault(n => n.VoucherID == id);
            ViewBag.VoucherID = voucher.VoucherID;
            if (voucher == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(voucher);
        }
        public ActionResult Edit(int id)
        {
            return View(database.Vouchers.SingleOrDefault(n => n.VoucherID == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                database.Entry(voucher).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Vouchers.SingleOrDefault(n => n.VoucherID == id));
        }
        [HttpPost]
        public ActionResult Delete(int id, Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                database.Entry(voucher).State = System.Data.Entity.EntityState.Deleted;
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}