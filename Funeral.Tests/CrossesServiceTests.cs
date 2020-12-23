using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.Services;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Funeral.Tests
{
    public class CrossesServiceTests
    {
        List<Cross> crossesList = new List<Cross>
            {
                new Cross
                {
                    Id = "1",
                    FilePath = "Path1",
                },
                new Cross
                {
                    Id = "2",
                    FilePath = "Path2"
                },
            };

        List<Obituary> obituariesList = new List<Obituary>
        {
            new Obituary
            {
                Id = "ObituaryId1",
                UserId = "UserId1",
                FrameId = "FrameId1",
                TextTemplateId = "TextTemplateId1",
                CustomTextId = " CustomTextId1",
                CrossId = "1",
                PictureId = "PictureId1",
                AfterCrossTextId = "AfterCrossTextId1",
                CrossTextId = "CrossTextId1",
                FromId = "FromId1",
                FullNameId = "FullNameId1",
                PanahidaId = "PanahidaId1",
                YearId = "YearId1",
            },
            new Obituary
            {
                Id = "ObituaryId2",
                UserId = "UserId2",
                FrameId = "FrameId2",
                TextTemplateId = "TextTemplateId2",
                CustomTextId = " CustomTextId2",
                CrossId = "3",
                PictureId = "PictureId2",
                AfterCrossTextId = "AfterCrossTextId2",
                CrossTextId = "CrossTextId2",
                FromId = "FromId1",
                FullNameId = "FullNameId2",
                PanahidaId = "PanahidaId2",
                YearId = "YearId2",
            }
        };

        private Mock<IEFRepository<Cross>> crossRepo;
        private Mock<IEFRepository<Obituary>> obituaryRepo;

        public CrossesServiceTests()
        {
            crossRepo = new Mock<IEFRepository<Cross>>();
            obituaryRepo = new Mock<IEFRepository<Obituary>>();

            var crossMock = crossesList.AsQueryable().BuildMock();
            crossRepo.Setup(method => method.All()).Returns(crossMock.Object);

            var obituaryMock = obituariesList.AsQueryable().BuildMock();
            obituaryRepo.Setup(method => method.All()).Returns(obituaryMock.Object);
        }
        
        [Fact]
        public void TestShowAllCrossesMethodReturnsPropertyValue()
        {                                
            var service = new CrossesService(crossRepo.Object, obituaryRepo.Object);
            var crossesCount = service.ShowAllCrosses().Count();

            Assert.Equal(2, crossesCount);
        }

        [Fact]
        public async Task TestGetCrossPathByIdAsyncMethod()
        {  
            
            var service = new CrossesService(crossRepo.Object, obituaryRepo.Object);
            var crossPath = await service.GetCrossPathByIdAsync("2");

            Assert.Equal("Path2", crossPath);
        }
    }
}
