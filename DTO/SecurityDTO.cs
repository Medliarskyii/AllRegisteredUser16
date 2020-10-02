using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
     public class SecurityDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Password { get; set; }
        public string KeyWord { get; set; }
        public bool IsTemporary { get; set; }

        public DateTime ExpiredDate { get; set; }

    }
}
