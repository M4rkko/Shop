using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge23.Core.Domain;
using ShopTARge23.ApplicationServices.Services;

namespace ShopTARge23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            KindergartenDto dto = new();

            dto.Size = 100;
            dto.Location = "asd";
            dto.RoomNumber = 1;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IKindergartenServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            //Act
            await Svc<IKindergartenServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            //Act
            await Svc<IKindergartenServices>().GetAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            KindergartenDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IKindergartenServices>().Create(realEstate);
            var result = await Svc<IKindergartenServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            KindergartenDto realEstate = MockRealEstateData();

            var realEstate1 = await Svc<IKindergartenServices>().Create(realEstate);
            var realEstate2 = await Svc<IKindergartenServices>().Create(realEstate);

            var result = await Svc<IKindergartenServices>().Delete((Guid)realEstate2.Id);

            Assert.NotEqual(result.Id, realEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = new Guid("8edd7b5d-822b-483d-ab81-048a638a2b31");

            KindergartenDto dto = MockRealEstateData();

            Kindergarten domain = new();

            domain.Id = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");
            domain.Size = 99;
            domain.Location = "qwe";
            domain.RoomNumber = 456;
            domain.BuildingType = "qwe";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            await Svc<IKindergartenServices>().Update(dto);

            Assert.Equal(guid, domain.Id);
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.NotEqual(dto.Size, domain.Size);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdatedataVersion2()
        {
            KindergartenDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto update = MockUpdateRealEstateData();
            var result = await Svc<IKindergartenServices>().Update(update);

            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
            Assert.NotEqual(result.ModifiedAt, createRealEstate.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            KindergartenDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto nullUpdate = MockNullRealEstateData();
            var result = await Svc<IKindergartenServices>().Update(nullUpdate);

            Assert.NotEqual(createRealEstate.Id, result.Id);
        }

        [Fact]
        public async Task Should_AddRealEstate_WhenInvalidData()
        {
            KindergartenDto dto = new()
            {
                Location = "",
                Size = -10,
                RoomNumber = 0,
                BuildingType = null,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            var result = await Svc<IKindergartenServices>().Create(dto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_CreateRealEstate_WithFutureDates()
        {
            // Arrange
            KindergartenDto futureDatedRealEstate = new()
            {
                Size = 150,
                Location = "kdkd",
                RoomNumber = 3,
                BuildingType = "öölkjk",
                CreatedAt = DateTime.Now.AddDays(1),
                ModifiedAt = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await Svc<IKindergartenServices>().Create(futureDatedRealEstate);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenUpdateAlreadyHappening()
        {
            var dto = MockRealEstateDataVersion2();
            var s = Svc<IKindergartenServices>();

            //double originalSize = (double)dto.Size;
            //int originalRoomNo = (int)dto.RoomNumber;

            var made = await s.Create(dto);
            made.RoomNumber = 424242;

            dto.Id = made.Id;
            dto.Size = -1000;

            var task1 = s.Update(dto);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                dto.RoomNumber = -1;
                var task2 = s.Update(dto);
                await task2;
            });
        }

        private KindergartenDto MockRealEstateData()
        {
            KindergartenDto realEstate = new()
            {
                Size = 100,
                Location = "asd",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }

        private KindergartenDto MockUpdateRealEstateData()
        {
            KindergartenDto realEstate = new()
            {
                Size = 44,
                Location = "vbn",
                RoomNumber = 6,
                BuildingType = "vbn",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return realEstate;
        }

        private KindergartenDto MockNullRealEstateData()
        {
            KindergartenDto realEstate = new()
            {
                Id = null,
                Size = 44,
                Location = "vbn",
                RoomNumber = 6,
                BuildingType = "vbn",
                CreatedAt = DateTime.Now.AddYears(-1),
                ModifiedAt = DateTime.Now.AddYears(-1),
            };

            return realEstate;
        }

        private KindergartenDto MockRealEstateDataVersion2()
        {
            return new()
            {
                Size = 1000,
                RoomNumber = 10,
                BuildingType = "Apartment",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
        }
    }
}