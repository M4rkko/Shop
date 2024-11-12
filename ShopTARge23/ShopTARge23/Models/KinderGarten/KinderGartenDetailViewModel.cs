using System;

namespace ShopTARge23.Models.KinderGarten
{
    public class KinderGartenDetailsViewModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string Teacher { get; set; }
        public List<KinderGartenImageViewModel> Image { get; set; }
            = new List<KinderGartenImageViewModel>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
