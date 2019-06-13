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
    public class CdDiscMenController : Controller
    {
        private SoundSharpDBEntities db = new SoundSharpDBEntities();
        // GET: CdDiscMen
        public ActionResult Index()
        {
            var cddiscmans = new List<VMCdDiscMan>();
            foreach (var mr in db.CdDiskMan)
            {
                // Create a new viewmodel
                var recorder = new VMCdDiscMan()
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
                    IsEjected = mr.IsEjected
                };
                cddiscmans.Add(recorder);
            }
            // Add the viewmodel to the list
            return View(cddiscmans);
        }

        // GET: CdDiscMen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.CdDiskMan.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMCdDiscMan()
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
                IsEjected = recorder.IsEjected
            };
            return View(mr);
        }

        // GET: CdDiscMen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CdDiscMen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialId,Make,Model,PriceExBtw,CreationDate,MbSize,DisplayWidth,DisplayHeight,TotalPixels,IsEjected")] VMCdDiscMan vMCdDiscMan)
        {
            if (ModelState.IsValid)
            {
                var device = new AudioDevices.AudioDevice()
                {
                    Make = vMCdDiscMan.Make,
                    Model = vMCdDiscMan.Model,
                    CreationDate = vMCdDiscMan.CreationDate,
                    PriceExBtw = vMCdDiscMan.PriceExBtw,
                    BtwPrecentage = (decimal)vMCdDiscMan.BtwPercentage,
                    SerialId = vMCdDiscMan.SerialId
                };

                var recorder = new CdDiskMan()
                {
                    MbSize = vMCdDiscMan.MbSize,
                    DisplayWidth = vMCdDiscMan.DisplayWidth,
                    DisplayHeight = vMCdDiscMan.DisplayHeight,
                    IsEjected = vMCdDiscMan.IsEjected,
                    AudioDevice = device,
                    AudioDeviceId = vMCdDiscMan.SerialId
                };
                db.AudioDevice.Add(device);
                db.CdDiskMan.Add(recorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vMCdDiscMan);
        }

        // GET: CdDiscMen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.CdDiskMan.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMCdDiscMan()
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
                IsEjected = recorder.IsEjected
            };

            return View(mr);
        }

        // POST: CdDiscMen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialId,Make,Model,PriceExBtw,CreationDate,MbSize,DisplayWidth,DisplayHeight,TotalPixels,IsEjected")] VMCdDiscMan vMCdDiscMan)
        {
            if (ModelState.IsValid)
            {
                var device = db.AudioDevice.Find(vMCdDiscMan.SerialId);
                device.Make = vMCdDiscMan.Make;
                device.Model = vMCdDiscMan.Model;
                device.CreationDate = vMCdDiscMan.CreationDate;
                device.PriceExBtw = vMCdDiscMan.PriceExBtw;
                device.BtwPrecentage = (decimal)vMCdDiscMan.BtwPercentage;
                device.SerialId = vMCdDiscMan.SerialId;

                var recorder = db.CdDiskMan.Find(vMCdDiscMan.SerialId);
                recorder.MbSize = vMCdDiscMan.MbSize;
                recorder.DisplayWidth = vMCdDiscMan.DisplayWidth;
                recorder.DisplayHeight = vMCdDiscMan.DisplayHeight;
                recorder.IsEjected = vMCdDiscMan.IsEjected;
                recorder.AudioDevice = device;

                db.Entry(device).State = EntityState.Modified;
                db.Entry(recorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vMCdDiscMan);
        }

        // GET: CdDiscMen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.CdDiskMan.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VMCdDiscMan()
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
                IsEjected = recorder.IsEjected
            };

            return View(mr);
        }

        // POST: CdDiscMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var recorder = db.CdDiskMan.Find(id);
            var device = db.AudioDevice.Find(id);

            db.CdDiskMan.Remove(recorder);
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
