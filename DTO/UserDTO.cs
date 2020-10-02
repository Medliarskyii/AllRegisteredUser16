using System;

namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public int RoleId { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public string BankCard { get; set; }


        public string GetInfo()
        {
            string info = Id.ToString() + " " + FirstName + " " + LastName + " " + Gender + " " + Address + " " + Mail + " " + Tel + " " + BankCard;
            return info;
        }


    }
    

}
