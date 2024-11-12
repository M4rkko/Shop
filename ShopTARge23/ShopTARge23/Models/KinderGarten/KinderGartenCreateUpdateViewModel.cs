using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopTARge23.Models.KinderGarten
{
    public class KinderGartenCreateUpdateViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public int ChildrenCount { get; set; }

        [Required]
        public string KindergartenName { get; set; }

        [Required]
        public string Teacher { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<KinderGartenImageViewModel> Image { get; set; }
            = new List<KinderGartenImageViewModel>();

    }
}
