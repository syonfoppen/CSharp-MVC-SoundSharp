using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AudioDevices;

namespace SoundSharpMVCWithDB.Models
{
    public class VMMp3Player
    {
        [Key]
        //set the set -get
        public int SerialId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PriceExBtw { get; set; }
        public double BtwPercentage { get; set; }
        public DateTime CreationDate { get; set; }
        public int? TrackListId { get; set; }
        public int MbSize { get; set; }
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }
        public TrackList TracklistNames { get; set; }
    }
}