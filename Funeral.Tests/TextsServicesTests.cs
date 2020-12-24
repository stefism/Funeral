using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.Services;
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
    public class TextsServicesTests
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

        List<TextTemplate> TextTemplatesList = new List<TextTemplate>
        {
            new TextTemplate
            {
                Id = "ttId1",
                Text = "Text test 1"
            },
            new TextTemplate
            {
                Id = "ttId2",
                Text = "Text test 2"
            }
        };

        private Mock<IEFRepository<TextTemplate>> textTemplateRepo;
        private Mock<IEFRepository<Obituary>> obituaryRepo;

        public TextsServicesTests()
        {
            obituaryRepo = new Mock<IEFRepository<Obituary>>();
            textTemplateRepo = new Mock<IEFRepository<TextTemplate>>();

            var obituaryMock = ObituariesList.AsQueryable().BuildMock();
            obituaryRepo.Setup(method => method.All()).Returns(obituaryMock.Object);

            var textsMock = TextTemplatesList.AsQueryable().BuildMock();
            textTemplateRepo.Setup(method => method.All()).Returns(textsMock.Object);
        }

        [Fact]
        public async Task TestAddTextToDbAsyncMethod()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("testDb");
            var virtualDbContext = new ApplicationDbContext(optionsBuilder.Options);
                       
            IEFRepository<TextTemplate> textTemplateRepository = new EFRepository<TextTemplate>(virtualDbContext);
            IEFRepository<Obituary> obituaryRepository = new EFRepository<Obituary>(virtualDbContext);

            var textService = new TextsService(textTemplateRepository, obituaryRepository);

            await textService.AddTextToDbAsync("text test 1");
            await textService.AddTextToDbAsync("text test 2");
            await textTemplateRepository.SaveChangesAsync();

            var dbCount = textTemplateRepository.All().Count();

            Assert.Equal(2, dbCount);
        }

        [Fact]
        public async Task TestDeleteTextTemplateAsyncMethod()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("testDb2");
            var virtualDbContext = new ApplicationDbContext(optionsBuilder.Options);

            IEFRepository<TextTemplate> textTemplateRepository = new EFRepository<TextTemplate>(virtualDbContext);
            IEFRepository<Obituary> obituaryRepository = new EFRepository<Obituary>(virtualDbContext);

            var textService = new TextsService(textTemplateRepository, obituaryRepository);

            await textService.AddTextToDbAsync("text test 1");
            await textService.AddTextToDbAsync("text test 2");
            await textTemplateRepository.SaveChangesAsync();

            var textId = textTemplateRepository.All()
                .Select(t => t.Id).FirstOrDefault();

            await textService.DeleteTextTemplateAsync(textId);
            await textTemplateRepository.SaveChangesAsync();

            var dbCount = textTemplateRepository.All().Count();

            Assert.Equal(1, dbCount);
        }

        [Fact]
        public void TestGetTextByIdMethod()
        {
            var textService = new TextsService(textTemplateRepo.Object, obituaryRepo.Object);
            var text = textService.GetTextById("ttId1");

            Assert.Equal("Text test 1", text);
        }

        [Fact]
        public void TestReturnFirtsTextMethod()
        {
            var textService = new TextsService(textTemplateRepo.Object, obituaryRepo.Object);
            var text = textService.ReturnFirtsText();

            Assert.Equal("Text test 1", text);
        }

        [Fact]
        public void TestShowAllTextsMethod()
        {
            var textService = new TextsService(textTemplateRepo.Object, obituaryRepo.Object);

            var textsCount = textService.ShowAllTexts().Count();

            Assert.Equal(2, textsCount);
        }

        [Fact]
        public async Task TestEditTextTemplateAsyncMethod()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("testDb3");
            var virtualDbContext = new ApplicationDbContext(optionsBuilder.Options);

            IEFRepository<TextTemplate> textTemplateRepository = new EFRepository<TextTemplate>(virtualDbContext);
            IEFRepository<Obituary> obituaryRepository = new EFRepository<Obituary>(virtualDbContext);

            var textService = new TextsService(textTemplateRepository, obituaryRepository);

            await textService.AddTextToDbAsync("text test 1");
            await textService.AddTextToDbAsync("text test 2");
            await textTemplateRepository.SaveChangesAsync();

            var textId = textTemplateRepository.All()
                .Select(t => t.Id).FirstOrDefault();
            await textService.EditTextTemplateAsync(textId, "new edited text");
            await textTemplateRepository.SaveChangesAsync();

            var text = textTemplateRepository.All()
                .Select(t => t.Text).FirstOrDefault();

            Assert.Equal("new edited text", text);
        }
    }
}
