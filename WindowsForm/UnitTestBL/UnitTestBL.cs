using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;

namespace UnitTestBL
{
    [TestClass]
    public class UnitTestBL
    {
        [TestMethod]
        public void TestGetCurrentUser()
        {
           
            Assert.AreEqual(LoginBL.SetCurrentUser("@gmail234"), false);
            Assert.AreEqual(LoginBL.SetCurrentUser("@gmail"), true);
            LoginBL.SetCurrentUser("@gmail");
            Assert.AreEqual(true,LoginBL.ChekPasswordForCurrentUser("1234"));
            Assert.AreEqual(false, LoginBL.ChekPasswordForCurrentUser("12349898"));
        }
    }
}
