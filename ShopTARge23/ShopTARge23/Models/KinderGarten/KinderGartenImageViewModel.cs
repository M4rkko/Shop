using System;
using System.Collections.Generic;
using ShopTARge23.Models.KinderGarten;

namespace ShopTARge23.Models.KinderGarten
{
    public class KinderGartenImageViewModel
    {
        public Guid ImageId { get; set; }
        public Guid KinderGartenId { get; set; }
        public byte[] ImageData { get; set; } 
        public string ImageTitle { get; set; }
        public string ImageBase64 { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
