using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public interface IUserDAL
    {
        UserDTO GetUserById(int id);
        List<UserDTO> GetAllUsers();
        UserDTO UpdateUser(UserDTO User);
        UserDTO CreateUser(UserDTO User);
        //void DeleteUser(int id);
       
    }

   

}
