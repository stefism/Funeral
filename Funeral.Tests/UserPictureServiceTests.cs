using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.Services;
using Funeral.App.ViewModels;
using Funeral.Data;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Funeral.Tests
{
    public class UserPictureServiceTests
    {
        List<Obituary> ObituariesList = new List<Obituary>
        {
            new Obituary
            {
                Id = "ObituaryId1",
                UserId = "UserId1",
                FrameId = "frame1Id",
                TextTemplateId = "ttId1",
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
        List<Picture> PicturesList = new List<Picture>
        {
            new Picture
            {
                Id = "pictureId1",
                FilePath = "picFilePath1",
                UserId = "userId1",
            },
            new Picture
            {
                Id = "pictureId2",
                FilePath = "picFilePath2",
                UserId = "userId1",
            },
             new Picture
            {
                Id = "pictureId3",
                FilePath = "picFilePath3",
                UserId = "userId2",
            }
        };

        private Mock<IEFRepository<Obituary>> obituaryRepo;
        private Mock<IEFRepository<Picture>> pictureRepo;

        public UserPictureServiceTests()
        {
            obituaryRepo = new Mock<IEFRepository<Obituary>>();
            pictureRepo = new Mock<IEFRepository<Picture>>();

            var obituaryMock = ObituariesList.AsQueryable().BuildMock();
            obituaryRepo.Setup(method => method.All()).Returns(obituaryMock.Object);

            var pictureMock = PicturesList.AsQueryable().BuildMock();
            pictureRepo.Setup(method => method.All()).Returns(pictureMock.Object);
        }

        [Fact]
        public void TestGetUserPictureCountMethod()
        {
            var userPictureService = new UserPictureService(pictureRepo.Object, obituaryRepo.Object);
            var count = userPictureService.GetUserPictureCount("userId1");

            Assert.Equal(2, count);
        }

        [Fact]
        public void TestAllUserPicturesMethod()
        {
            var userPictureService = new UserPictureService(pictureRepo.Object, obituaryRepo.Object);
            var count = userPictureService.AllUserPictures("userId1").Count();

            Assert.Equal(2, count);
        }

        [Fact]
        public void TestCurrentUserPicureMethod()
        {
            var userPictureService = new UserPictureService(pictureRepo.Object, obituaryRepo.Object);
            var picture = userPictureService.CurrentUserPicure("pictureId3");
            
            var viewModel = new UserPictureViewModel
            {
                PictureId = "pictureId3",
                PictureFilePath = "picFilePath3"
            };

            Assert.Equal(viewModel.PictureId, picture.PictureId);
            Assert.Equal(viewModel.PictureFilePath, picture.PictureFilePath);
        }

        [Fact]
        public async Task TestRemovePictureIdFromUserObityarysAsyncMethod()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("testDb4");
            var virtualDbContext = new ApplicationDbContext(optionsBuilder.Options);

            var obituaryRepository = new EFRepository<Obituary>(virtualDbContext);
            var pictureRepository = new EFRepository<Picture>(virtualDbContext);

            var userPictureService = new UserPictureService(pictureRepository, obituaryRepository);

            var obituary = new Obituary
            {
                Id = "ObituaryId1",
                UserId = "UserId1",
                FrameId = "frame1Id",
                TextTemplateId = "ttId1",
                CustomTextId = " CustomTextId1",
                CrossId = "1",
                PictureId = "PictureId1",
                AfterCrossTextId = "AfterCrossTextId1",
                CrossTextId = "CrossTextId1",
                FromId = "FromId1",
                FullNameId = "FullNameId1",
                PanahidaId = "PanahidaId1",
                YearId = "YearId1",
            };

            await obituaryRepository.AddAsync(obituary);
            await obituaryRepository.SaveChangesAsync();

            await userPictureService
                .RemovePictureIdFromUserObityarysAsync("PictureId1");

            var dbObituary = obituaryRepository.All().FirstOrDefault();

            Assert.Null(dbObituary.PictureId);
        }
    }
}
