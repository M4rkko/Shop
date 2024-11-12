using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Models.KinderGarten;


namespace ShopTARgv23.Controllers
{
    public class KinderGartensController : Controller
    {
        private readonly ShopTARge23Context _context;
        private readonly IKinderGartenServices _kinderGartenServices;
        private readonly IFileServices _fileServices;

        public KinderGartensController
            (
                ShopTARge23Context context,
                IKinderGartenServices kinderGartenServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _kinderGartenServices = kinderGartenServices;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.KinderGartens
                .Select(x => new KinderGartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    Teacher = x.Teacher
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KinderGartenCreateUpdateViewModel kinderGarten = new();

            return View("CreateUpdate", kinderGarten);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KinderGartenCreateUpdateViewModel vm)
        {
            var dto = new KinderGartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                Teacher = vm.Teacher,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Images = vm.Images
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KinderGartenId = x.KinderGartenId
                    }).ToArray()
            };

            var result = await _kinderGartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.GetAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new KinderGartenImageViewModel
                {
                    KindergartenId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KinderGartenCreateUpdateViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.Teacher = kinderGarten.Teacher;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Images.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KinderGartenCreateUpdateViewModel vm)
        {
            var dto = new KinderGartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                Teacher = vm.Teacher,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Images = vm.Images.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KinderGartenId = x.KindergartenId,
                }).ToArray()
            };

            var result = await _kinderGartenServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.GetAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new KinderGartenImageViewModel
                {
                    KindergartenId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KinderGartenDetailsViewModel();

            vm.Id = id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.Teacher = kinderGarten.Teacher;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Images.AddRange(photos);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.GetAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new KinderGartenImageViewModel
                {
                    KindergartenId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KinderGartenDeleteViewModel();

            vm.Id = id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.Teacher = kinderGarten.Teacher;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Images.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.Delete(id);

            if (kinderGarten == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(KinderGartenImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

