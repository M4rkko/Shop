using System;
using Microsoft.AspNetCore.Http;

namespace ShopTARge23.Core.Dto
{
    public class KinderGartenDto
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? Teacher { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public IEnumerable<FileToDatabaseDto> Image { get; set; }
    = new List<FileToDatabaseDto>();
    }
}