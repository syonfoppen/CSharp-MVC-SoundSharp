using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AudioDevices;

namespace SoundSharpMVCWithDB.Controllers
{
    public class TrackListsController : Controller
    {
        private SoundSharpDBEntities db = new SoundSharpDBEntities();

        // GET: TrackLists
        public ActionResult Index()
        {
            
            ViewBag.Tracks = new SelectList(db.Track, "ID", "Name");
            return View(db.TrackList.ToList());
        }

        // GET: TrackLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackList trackList = db.TrackList.Find(id);
            if (trackList == null)
            {
                return HttpNotFound();
            }
            return View(trackList);
        }

        // GET: TrackLists/Create
        public ActionResult Create()
        {
            var Tracklist = db.Track.ToList();
            ViewBag.Tracklist = new SelectList(Tracklist, "ID", "Name");

            return View();
        }

        // POST: TrackLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Owner,Track")] TrackList trackList)
        {
            if (ModelState.IsValid)
            {

                if (trackList != null)
                {
                    var tracks = Request.Form["Select State"];
                    string[] trackslist = tracks.Split(',');
                    foreach (var id in trackslist)
                    {
                        Track track = db.Track.Find(int.Parse(id));
                        trackList.Track.Add(track);
                    }
                    db.TrackList.Add(trackList);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(trackList);
        }

        // GET: TrackLists/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackList trackList = db.TrackList.Find(id);
            if (trackList == null)
            {
                return HttpNotFound();
            }
            var Tracklist = db.Track.ToList();
            ViewBag.Tracklist = new SelectList(Tracklist, "ID", "Name");
            return View(trackList);
        }

        // POST: TrackLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Owner,Track")] TrackList trackList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trackList).State = EntityState.Modified;
                db.SaveChanges();
                var tracks = Request.Form["Select State1"];
                if (tracks != null)
                {
                    string[] trackslist = tracks.Split(',');
                    db.spDeleteTrackFromTracklist(trackList.ID);
                    db.SaveChanges();
                    foreach (var id in trackslist)
                    {    
                        db.spAddTrackToList(int.Parse(id), trackList.ID);          
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(trackList);
        }

        // GET: TrackLists/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackList trackList = db.TrackList.Find(id);
            if (trackList == null)
            {
                return HttpNotFound();
            }
            return View(trackList);
        }

        // POST: TrackLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrackList trackList = db.TrackList.Find(id);

            db.TrackList.Remove(trackList);
            
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
