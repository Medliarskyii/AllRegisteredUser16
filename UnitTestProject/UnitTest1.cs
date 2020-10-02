using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using DTO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodCheckUser()
        {
            string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
            UserDAL dal = new UserDAL(connStr);
            UserDTO currentUser = null;
            currentUser = dal.GetUserById(4);

            Assert.AreEqual(currentUser.FirstName, "wom");
        }

        [TestMethod]
        public void TestMethodCheckUserUpdate()
        {
            string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
            UserDAL dal = new UserDAL(connStr);
            UserDTO currentUser = null;
            currentUser = dal.GetUserById(4);
            currentUser.FirstName = "wom1";
            currentUser = dal.UpdateUser(currentUser);

            Assert.AreEqual(currentUser.FirstName, "wom1");
        }


        [TestMethod]
        public void CheckUsersCount()
        {
            string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
            UserDAL dal = new UserDAL(connStr);
            int count = dal.GetAllUsers().Count;

            Assert.AreEqual(count,3);
        }
        

    }
}
