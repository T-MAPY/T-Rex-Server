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
            Assert.IsTrue(output.Equals("3QJxHj85xt1qj11QHWvNuA=="));
        }

        [TestMethod]
        public void TestCheckStatusDto()
        {
            var dataDto = new StatusDTO() {i = "deviceA", l = null, o = null, t = null, k = ""};
            //Assert.IsFalse(SecurityHelper.CheckAccessKey(dataDto)); 

            dataDto.k = SecurityHelper.GetMd5Hash("deviceA" + "Trex1Security2String3");
            //Assert.IsTrue(SecurityHelper.CheckAccessKey(dataDto));

            var date = DateTime.Now;
            dataDto.t = date;
            dataDto.k = SecurityHelper.GetMd5Hash("deviceA" + date.ToString("yyyy-MM-dd'T'HH:mm:ss") + "Trex1Security2String3");
            //Assert.IsTrue(SecurityHelper.CheckAccessKey(dataDto));
        }
    }
}
