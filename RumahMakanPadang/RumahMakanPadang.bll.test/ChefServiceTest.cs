using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using RumahMakanPadang.bll.test.Common;
using RumahMakanPadang.dal.Models;
using RumahMakanPadang.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RumahMakanPadang.bll.test
{
    public class ChefServiceTest
    {
        private IEnumerable<Chef> chefs;
        //private Mock<IRedisService> redis;
        private Mock<IUnitOfWork> uow;

        public ChefServiceTest()
        {
            chefs = CommonHelper.LoadDataFromFile<IEnumerable<Chef>>("../../../MockData/Chef.json");
            uow = MockUnitOfWork();
            //redis = MockRedis();
        }

        private ChefService CreateChefService()
        {
            return new ChefService(uow.Object);
        }

        private Mock<IUnitOfWork> MockUnitOfWork()
        {
            var chefsQueryable = chefs.AsQueryable().BuildMock().Object;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            //Setup for getall
            mockUnitOfWork
                .Setup(u => u.ChefRepository.GetAll())
                .Returns(chefsQueryable);

            //Setup for Querying isexist in repo
            mockUnitOfWork
                .Setup(u => u.ChefRepository.IsExist(It.IsAny<Expression<Func<Chef, bool>>>()))
                .Returns((Expression<Func<Chef, bool>> condition) => chefsQueryable.Any(condition));

            //Setup for Querying single object in repo
            mockUnitOfWork
               .Setup(u => u.ChefRepository.GetSingleAsync(It.IsAny<Expression<Func<Chef, bool>>>()))
               .ReturnsAsync((Expression<Func<Chef, bool>> condition) => chefsQueryable.FirstOrDefault(condition));

            //Setup for adding new object in repo
            mockUnitOfWork
               .Setup(u => u.ChefRepository.AddAsync(It.IsAny<Chef>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Chef chef, CancellationToken token) =>
               {
                   chef.Id = Guid.NewGuid();
                   return chef;
               });

            //Setup for delete
            mockUnitOfWork
                .Setup(u => u.ChefRepository.Delete(It.IsAny<Expression<Func<Chef, bool>>>()))
                .Verifiable();

            //Setup for save
            mockUnitOfWork
                .Setup(x => x.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return mockUnitOfWork;
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            //arrange
            var expected = chefs;

            var svc = CreateChefService();

            // act
            var actual = await svc.GetAllChefAsync();

            // assert      
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("11")]
        [InlineData("12")]
        public async Task GetByKTP_Success(string ktp)
        {
            //arrange
            var expected = chefs.First(x => x.KTP == ktp);

            var svc = CreateChefService();

            //act
            var actual = await svc.GetChefByKTPAsync(ktp);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("01")]
        [InlineData("02")]
        public async Task GetByKTP_Failed(string ktp)
        {
            //arrange
            //var expected = chefs.First(x => x.KTP == ktp);

            var svc = CreateChefService();

            //act
            var actual = await svc.GetChefByKTPAsync(ktp);

            //assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task CreateChef_Success()
        {
            //arrange
            var expected = new Chef
            {
                Nama = "Test",
                KTP = "8",

            };

            var svc = CreateChefService();

            //act
            Func<Task> act = async () => { await svc.CreateChefAsync(expected); };

            await act.Should().NotThrowAsync<Exception>();

            //assert
            uow.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
