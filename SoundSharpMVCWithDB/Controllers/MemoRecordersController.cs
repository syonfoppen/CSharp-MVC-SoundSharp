using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AudioDevices;
using SoundSharpMVCWithDB.ViewModel;

namespace SoundSharpMVCWithDB.Controllers
{
    public class MemoRecordersController : Controller
    {
        private SoundSharpDBEntities db = new SoundSharpDBEntities();

        // GET: MemoRecorders
        public ActionResult Index()
        {
            var memorecorders = new List<VmMemoRecorder>();
            foreach (var mr in db.MemoRecorder)
            {
                // Create a new viewmodel
                var recorder = new VmMemoRecorder()
                {
                    Make = mr.AudioDevice.Make,
                    Model = mr.AudioDevice.Model,
                    CreationDate = mr.AudioDevice.CreationDate,
                    PriceExBtw = mr.AudioDevice.PriceExBtw,
                    BtwPercentage = (double)mr.AudioDevice.BtwPrecentage,
                    SerialId = mr.AudioDevice.SerialId,
                    MaxMemoCartridgeType = mr.MaxCatridgetype
                };
                memorecorders.Add(recorder);
            }
            // Add the viewmodel to the list
            return View(memorecorders);
        }

        // GET: MemoRecorders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCatridgetype
            };
            return View(mr);
        }

        // GET: MemoRecorders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemoRecorders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MaxMemoCartridgeType")] VmMemoRecorder vmMemoRecorder)
        {
            if (ModelState.IsValid)
            {
                var device = new AudioDevices.AudioDevice()
                {
                    Make = vmMemoRecorder.Make,
                    Model = vmMemoRecorder.Model,
                    CreationDate = vmMemoRecorder.CreationDate,
                    PriceExBtw = vmMemoRecorder.PriceExBtw,
                    BtwPrecentage = (decimal)vmMemoRecorder.BtwPercentage,
                    SerialId = vmMemoRecorder.SerialId
                };

                var recorder = new MemoRecorder()
                {
                    MaxCatridgetype = vmMemoRecorder.MaxMemoCartridgeType,
                    AudioDevice = device,
                    AudioDeviceId = vmMemoRecorder.SerialId
                };
                db.AudioDevice.Add(device);
                db.MemoRecorder.Add(recorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vmMemoRecorder);
        }

        // GET: MemoRecorders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCatridgetype
            };

            return View(mr);
        }

        // POST: MemoRecorders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialId,Make,Model,CreationDate,PriceExBtw,BtwPercentage,MaxMemoCartridgeType")] VmMemoRecorder vmMemoRecorder)
        {
            if (ModelState.IsValid)
            {
                var device = db.AudioDevice.Find(vmMemoRecorder.SerialId);
                device.Make = vmMemoRecorder.Make;
                device.Model = vmMemoRecorder.Model;
                device.CreationDate = vmMemoRecorder.CreationDate;
                device.PriceExBtw = vmMemoRecorder.PriceExBtw;
                device.BtwPrecentage = (decimal)vmMemoRecorder.BtwPercentage;
                device.SerialId = vmMemoRecorder.SerialId;

                var recorder = db.MemoRecorder.Find(vmMemoRecorder.SerialId);
                recorder.MaxCatridgetype = vmMemoRecorder.MaxMemoCartridgeType;
                recorder.AudioDevice = device;

                db.Entry(device).State = EntityState.Modified;
                db.Entry(recorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmMemoRecorder);
        }

        // GET: MemoRecorders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recorder = db.MemoRecorder.Find(id);
            if (recorder == null)
            {
                return HttpNotFound();
            }
            var mr = new VmMemoRecorder()
            {
                Make = recorder.AudioDevice.Make,
                Model = recorder.AudioDevice.Model,
                CreationDate = recorder.AudioDevice.CreationDate,
                PriceExBtw = recorder.AudioDevice.PriceExBtw,
                BtwPercentage = (double)recorder.AudioDevice.BtwPrecentage,
                SerialId = recorder.AudioDevice.SerialId,
                MaxMemoCartridgeType = recorder.MaxCatridgetype
            };

            return View(mr);
        }

        // POST: MemoRecorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var recorder = db.MemoRecorder.Find(id);
            var device = db.AudioDevice.Find(id);

            db.MemoRecorder.Remove(recorder);
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
