using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SoundSharpMVCWithDB.ViewModel
{
    public class VmMemoRecorder
    {
        [Key]
        //set the set -get
        public int SerialId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal PriceExBtw { get; set; }
        public double BtwPercentage { get; set; }
        public string MaxMemoCartridgeType { get; set; }
    }
}