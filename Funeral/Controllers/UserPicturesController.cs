﻿using Funeral.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    [Authorize]
    public class UserPicturesController : Controller
    {
        private readonly IUserPictureService userPictureService;
        private readonly IFileService fileService;
        private readonly IUserService userService;

        private string UserId =>
            User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public UserPicturesController(
            IUserPictureService userPictureService, 
            IFileService fileService,
            IUserService userService)
        {
            this.userPictureService = userPictureService;
            this.fileService = fileService;
            this.userService = userService;
        }

        public IActionResult AllUserPictures()
        {
            var viewModel = userPictureService.AllUserPictures(UserId);
            
            return View(viewModel);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult AllUserPicturesAdmin(string id)
        {
            var viewModel = userPictureService.AllUserPictures(id);
            TempData["UserName"] = userService.GetUserNameById(id);
            
            return View(viewModel);
        }

        public IActionResult DeleteUserPicture(string pictureId)
        {
            var viewModel = userPictureService.CurrentUserPicure(pictureId);

            return View(viewModel);
        }

        public async Task<IActionResult> DoDeleteUserPicture(string pictureId)
        {
            await userPictureService.RemovePictureIdFromUserObityarysAsync(pictureId);
            await fileService.DeleteUserPictureFileAsync(pictureId);

            return RedirectToAction(nameof(AllUserPictures));
        }
    }
}
