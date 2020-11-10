using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Drawing;

namespace BL
{
    public static class LoginBL
    {

        static string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
        static UserDAL Dal;
        public static UserDTO CurrentUser;
       

        public static bool SetCurrentUser(string usermail)
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }
            CurrentUser = Dal.GetUserByEmail(usermail);
            if (CurrentUser != null)
                return true;
            return false;
        }

        public static bool SetCurrentUser(int userId)
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }
            CurrentUser = Dal.GetUserById(userId);
            if (CurrentUser != null)
                return true;
            return false;
        }

        public static bool ChekPasswordForCurrentUser(string password)
        {
            if (Dal == null)
            {
                
                Dal = new UserDAL(connStr);
            }
 
            return Dal.ChekPassword(CurrentUser, password);
        }


        public static bool CreateNewUser(UserDTO u, SecurityDTO s, string i)
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }
           
                CurrentUser = Dal.CreateUser(u);
                s.UserId = CurrentUser.Id;
                Dal.SetPasswordForUser(s);
                if (!String.IsNullOrEmpty(i))
                        Dal.PutImageBinaryInDb(i, s.UserId);
                return true;
            
            
        }


        public static void UpdateUser(UserDTO u, SecurityDTO s, string i)
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }

                u.Id = CurrentUser.Id;
                CurrentUser = Dal.UpdateUser(u);
                s.UserId = CurrentUser.Id;
                Dal.UpdatePasswordForUser(s);
                if (!String.IsNullOrEmpty(i))
                    Dal.PutImageBinaryInDb(i, s.UserId);
            

        }
        public static Image GetImageBinaryFromDb(string userId)
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }

            return Dal.GetImageBinaryFromDb(userId);
        }
    }
}
