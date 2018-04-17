using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinPlannerDataFirst.Models;
using FinPlannerDataFirst.Models.CustomAttr;
using FinPlannerDataFirst.Models.Helpers;
using Microsoft.AspNet.Identity;

namespace FinPlannerDataFirst.Controllers
{
    public class InvitesController : Controller
    {
        private DBConnect db = new DBConnect();

        // GET: Invites
        public ActionResult Index()
        {
            var invites = db.Invites.Include(i => i.Household);
            return View(invites.ToList());
        }

        //// GET: Invites/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Invite invite = db.Invites.Find(id);
        //    if (invite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(invite);
        //}

        // POST: Invites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeHouseholdRequired]
        public async Task<ActionResult> Create(string email, string fName, string lName)
        {
            Invite inv = new Invite() {
                Email = email,
                HasBeenUsed = false,
                InviteDate = DateTimeOffset.Now,
                HHToken = new Guid(),
                InvitedById = User.Identity.GetUserId()
            };

            //FIX THIS - should be able to save the full name through the email and then cause it to become the fullname of the new joinee.  
            string fullName = fName + " " + lName;

            EmailSender es = new EmailSender();
            //FIX THIS - should send user to household dash
            var callbackUrl = Url.Action("Details", "Tickets", null, protocol: Request.Url.Scheme);

            await es.SendInviteNoti(User.Identity.Name, callbackUrl, fName, inv.Email, inv.HHToken);

            db.Invites.Add(inv);
            db.SaveChanges();

            // FIX THIS - redirect to household dash
            return RedirectToAction("Index");
        }

        // GET: Invites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invite invite = db.Invites.Find(id);
            if (invite == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
            return View(invite);
        }

        // POST: Invites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Email,HHToken,InviteDate,InvitedById,HasBeenUsed")] Invite invite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
            return View(invite);
        }

        // GET: Invites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invite invite = db.Invites.Find(id);
            if (invite == null)
            {
                return HttpNotFound();
            }
            return View(invite);
        }

        // POST: Invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invite invite = db.Invites.Find(id);
            db.Invites.Remove(invite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
