using Funeral.Controllers;
using Funeral.Web.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Funeral.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void TestAllUsersInfo()
            => MyMvc
            .Controller<HomeController>()
            .Calling(c => c.Index())
            .ShouldReturn()
            .View();
    }
}
