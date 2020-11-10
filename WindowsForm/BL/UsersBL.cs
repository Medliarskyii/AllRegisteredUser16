using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BL
{
    public static class UsersBL

    {

        static string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
        static UserDAL Dal;
        public static UserDTO CurrentUser;

        public static List<UserDTO> GetAllUsers()
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }
            return Dal.GetAllUsers();
        }

        public static SecurityDTO GetSecurityForUser(UserDTO userDTO)/// + add loginName +///
        {
            if (Dal == null)
            {

                Dal = new UserDAL(connStr);
            }
            return Dal.GetSecurityForUser(userDTO);
        }
    }
}
