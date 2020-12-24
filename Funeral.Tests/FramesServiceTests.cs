using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.Services;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Funeral.Tests
{
    public class FramesServiceTests
    {
        List<Frame> FramesList = new List<Frame>
        {
            new Frame
            {
                Id = "frame1Id",
                FilePath = "framePath1"
            },
            new Frame
            {
                Id = "frame2Id",
                FilePath = "framePath2"
            },
        };

        List<Obituary> ObituariesList = new List<Obituary>
        {
            new Obituary
            {
                Id = "ObituaryId1",
                UserId = "UserId1",
                FrameId = "frame1Id",
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
                FrameId = "FrameId3",
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

        private Mock<IEFRepository<Frame>> framesRepo;
        private Mock<IEFRepository<Obituary>> obituaryRepo;

        public FramesServiceTests()
        {
            framesRepo = new Mock<IEFRepository<Frame>>();
            obituaryRepo = new Mock<IEFRepository<Obituary>>();

            var framesMock = FramesList.AsQueryable().BuildMock();
            framesRepo.Setup(method => method.All()).Returns(framesMock.Object);

            var obituaryMock = ObituariesList.AsQueryable().BuildMock();
            obituaryRepo.Setup(method => method.All()).Returns(obituaryMock.Object);
        }

        [Fact]
        public void TestShowAllFramesMethod()
        {
            var service = new FramesService(framesRepo.Object, obituaryRepo.Object);
            var framesCount = service.ShowAllFrames().Count();

            Assert.Equal(2, framesCount);
        }

        [Fact]
        public void TestGetFramePathByIdMethod()
        {
            var service = new FramesService(framesRepo.Object, obituaryRepo.Object);
            var framePath = service.GetFramePathById("frame1Id");

            Assert.Equal("framePath1", framePath);
        }
    }
}
