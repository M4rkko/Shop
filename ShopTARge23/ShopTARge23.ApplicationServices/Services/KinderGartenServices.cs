using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;

namespace ShopTARge23.ApplicationServices.Services
{
    public class KinderGartenServices : IKinderGartenServices
    {
        private readonly ShopTARge23Context _context;
        private readonly IFileServices _fileServices;

        public KinderGartenServices
            (
                ShopTARge23Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<KinderGarten> Create(KinderGartenDto dto)
        {
            KinderGarten kinderGarten = new();

            kinderGarten.Id = Guid.NewGuid();
            kinderGarten.Size = dto.Size;
            kinderGarten.Location = dto.Location;
            kinderGarten.RoomNumber = dto.RoomNumber;
            kinderGarten.BuildingType = dto.BuildingType;
            kinderGarten.CreatedAt = DateTime.Now;
            kinderGarten.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kinderGarten);
            }

            await _context.RealEstates.AddAsync(kinderGarten);
            await _context.SaveChangesAsync();

            return kinderGarten;
        }

        public async Task<KinderGarten> GetAsync(Guid id)
        {
            var result = await _context.KinderGarten
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<KinderGarten> Update(KinderGartenDto dto)
        {
            KinderGarten domain = new();

            domain.Id = dto.Id;
            domain.Size = dto.Size;
            domain.Location = dto.Location;
            domain.BuildingType = dto.BuildingType;
            domain.RoomNumber = dto.RoomNumber;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            _context.KinderGarten.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<KinderGarten> Delete(Guid id)
        {
            var result = await _context.KinderGarten
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.KinderGartenId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    RealEstateId = y.KinderGartenId
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);
            _context.KinderGarten.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
