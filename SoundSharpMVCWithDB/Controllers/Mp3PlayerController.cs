using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AudioDevices;
using SoundSharpMVCWithDB.Models;

namespace SoundSharpMVCWithDB.Controllers
{
    public class Mp3PlayerController : Controller
    {
        private SoundSharpDBEntities db = new SoundSharpDBEntities();

        // GET: Mp3Player
        public ActionResult Index()
        {
            var tracklistname = db.TrackList.Include(t => t.Name);
            var mp3players = new List<VMMp3Player>();
            foreach (var mr in db.Mp3Player)
            {
                // Create a new viewmodel
                
                var recorder = new VMMp3Player()
                {
                    
                    Make = mr.AudioDevice.Make,
                    Model = mr.AudioDevice.Model,
                    CreationDate = mr.AudioDevice.CreationDate,
                    PriceExBtw = mr.AudioDevice.PriceExBtw,
                    BtwPercentage = (double)mr.AudioDevice.BtwPrecentage,
                    SerialId = mr.AudioDevice.SerialId,
                    MbSize = mr.MbSize,
                    DisplayWidth = mr.DisplayWidth,
                    DisplayHeight = mr.DisplayHeight,
                    TrackListId = mr.TrackList,
                    TracklistNames = mr.TrackList1,

                };
                mp3players.Add(recorder);
            }
            // Add the viewmodel to the list
            return View(mp3players);
        }

        // GET: Mp3Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.Mp3Player.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMMp3Player()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MbSize = recorder.MbSize,
                DisplayWidth = recorder.DisplayWidth,
                DisplayHeight = recorder.DisplayHeight,
                TrackListId = recorder.TrackList,
                TracklistNames = recorder.TrackList1,
                
            };
            return View(mr);
        }

        // GET: Mp3Player/Create
        public ActionResult Create()
        {
            ViewBag.TracklistId = new SelectList(db.TrackList, "ID", "Name");
            return View();
        }

        // POST: Mp3Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialId,Make,Model,PriceExBtw,BtwPercentage,CreationDate,TracklistId,MbSize,DisplayWidth,DisplayHeight")] VMMp3Player vMMp3Player)
        {
            if (ModelState.IsValid)
            {
                var device = new AudioDevices.AudioDevice()
                {
                    Make = vMMp3Player.Make,
                    Model = vMMp3Player.Model,
                    CreationDate = vMMp3Player.CreationDate,
                    PriceExBtw = vMMp3Player.PriceExBtw,
                    BtwPrecentage = (decimal)vMMp3Player.BtwPercentage,
                    SerialId = vMMp3Player.SerialId

                };

                var recorder = new Mp3Player()
                {
                    MbSize = vMMp3Player.MbSize,
                    DisplayWidth = vMMp3Player.DisplayWidth,
                    DisplayHeight = vMMp3Player.DisplayHeight,
                    TrackList = vMMp3Player.TrackListId,
                    AudioDevice = device
                };
                db.AudioDevice.Add(device);
                db.Mp3Player.Add(recorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TracklistId = new SelectList(db.TrackList, "ID", "Name", vMMp3Player.TrackListId);
            return View(vMMp3Player);
        }

        // GET: Mp3Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.Mp3Player.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMMp3Player()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MbSize = recorder.MbSize,
                DisplayWidth = recorder.DisplayWidth,
                DisplayHeight = recorder.DisplayHeight,
                TrackListId = recorder.TrackList
            };
            ViewBag.TracklistId = new SelectList(db.TrackList, "ID", "Name", mr.TrackListId);
            return View(mr);
        }

        // POST: Mp3Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialId,Make,Model,PriceExBtw,BtwPercentage,CreationDate,TrackListId,MbSize,DisplayWidth,DisplayHeight")] VMMp3Player vMMp3Player)
        {
            if (ModelState.IsValid)
            {
                var device = db.AudioDevice.Find(vMMp3Player.SerialId);
                device.Make = vMMp3Player.Make;
                device.Model = vMMp3Player.Model;
                device.CreationDate = vMMp3Player.CreationDate;
                device.PriceExBtw = vMMp3Player.PriceExBtw;
                device.BtwPrecentage = (decimal)vMMp3Player.BtwPercentage;
                device.SerialId = vMMp3Player.SerialId;

                var recorder = db.Mp3Player.Find(vMMp3Player.SerialId);
                recorder.MbSize = vMMp3Player.MbSize;
                recorder.DisplayWidth = vMMp3Player.DisplayWidth;
                recorder.DisplayHeight = vMMp3Player.DisplayHeight;
                recorder.TrackList = vMMp3Player.TrackListId;
                recorder.AudioDevice = device;

                db.Entry(device).State = EntityState.Modified;
                db.Entry(recorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TracklistId = new SelectList(db.TrackList, "ID", "Name", vMMp3Player.TrackListId);
            return View(vMMp3Player);
        }

        // GET: Mp3Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.Mp3Player.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMMp3Player()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MbSize = recorder.MbSize,
                DisplayWidth = recorder.DisplayWidth,
                DisplayHeight = recorder.DisplayHeight,
                TrackListId = recorder.TrackList
            };

            return View(mr);
        }

        // POST: Mp3Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var recorder = db.Mp3Player.Find(id);
            var device = db.AudioDevice.Find(id);

            db.Mp3Player.Remove(recorder);
            db.AudioDevice.Remove(device);
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
