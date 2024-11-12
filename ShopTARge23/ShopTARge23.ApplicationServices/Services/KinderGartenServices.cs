using Microsoft.EntityFrameworkCore;
using ShopTARge23.ApplicationServices.Services;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Data.Migrations;
using System;
using System.Threading.Tasks;

namespace ShopTARge23.ApplicationServices.Services
{
    public class KinderGartenServices : IKinderGartenServices
    {
        private readonly ShopTARge23Context _context;

        public KinderGartenServices(ShopTARge23Context context)
        {
            _context = context;
        }


        }
        public async Task<KinderGarten> Create(KinderGartenDto dto)
        {
        KinderGarten kinderGarten = new();

    kinderGarten.Id = Guid.NewGuid(),
                kinderGarten.GroupName = dto.GroupName,
                kinderGarten.ChildrenCount = dto.ChildrenCount,
                kinderGarten.KindergartenName = dto.KindergartenName,
                kinderGarten.Teacher = dto.Teacher,
                kinderGarten.CreatedAt = DateTime.Now,
                kinderGarten.UpdatedAt = DateTime.Now

        if (dto.Files != null)
        {
        _fileServices.UploadFilesToDatabase(dto, kinderGarten);
         }


            await _context.KinderGarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<KinderGarten> GetAsync(Guid id)
        {
            return await _context.KinderGarten
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<KinderGarten> Update(KinderGartenDto dto)
        {
            var kindergarten = await _context.KinderGarten
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (kindergarten != null)
            {
                kindergarten.GroupName = dto.GroupName;
                kindergarten.ChildrenCount = dto.ChildrenCount;
                kindergarten.KindergartenName = dto.KindergartenName;
                kindergarten.Teacher = dto.Teacher;
                kindergarten.UpdatedAt = DateTime.Now;

                _context.KinderGarten.Update(kindergarten);
                await _context.SaveChangesAsync();
            }

            return kindergarten;
        }

        public async Task<KinderGarten> Delete(Guid id)
        {
            var kindergarten = await _context.KinderGarten
                .FirstOrDefaultAsync(x => x.Id == id);

            if (kindergarten != null)
            {
                _context.KinderGarten.Remove(kindergarten);
                await _context.SaveChangesAsync();
            }

            return kindergarten;
        }
    }
}