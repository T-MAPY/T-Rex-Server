using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TRexServer.Models;
using TRexServer.Security;

namespace TRexServerTests
{
    [TestClass]
    public class SecurityHelperTests
    {
        [TestMethod]
        public void TestGetMd5Hash()
        {
            var inputString = "Trex1Security2String3";
            var output = SecurityHelper.GetMd5Hash(inputString);
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void TestCheckStatusDto()
        {
            var dataDto = new StatusDTO() {c = ""};
            //Assert.IsFalse(SecurityHelper.CheckStatusDto(dataDto)); //je potřeba do configu nastavit řetězec
            dataDto.c = "3QJxHj85xt1qj11QHWvNuA==";
            Assert.IsTrue(SecurityHelper.CheckStatusDto(dataDto));
        }
    }
}
