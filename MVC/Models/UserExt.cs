using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
#nullable disable

namespace WebCoreKino5.Models
{
    public  class UserExt : User
    {
        public IFormFile Avatar { get; set; }

        public UserExt()
        {
        }

        public UserExt(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Mail = user.Mail;
            Password = user.Password;
            KeyWord = user.KeyWord;
            ExpiredDate = user.ExpiredDate;
            IsTemporary = user.IsTemporary;
            Image = user.Image;
            Image2 = user.Image2;
            Description = user.Description;
            RoleId = user.RoleId;

        }

        public User GetUser()
        {
            User user = new User();
            user.Id = Id;
            user.Name = Name;
            user.Surname = Surname;
            user.Mail = Mail;
            user.Password = Password;
            user.KeyWord = KeyWord;
            user.ExpiredDate = ExpiredDate;
            user.IsTemporary = IsTemporary;
            user.Image = Image;
            user.Image2 = Image2;
            user.Description = Description;
            user.RoleId = RoleId;  
            
            if (Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)Avatar.Length);
                }
                // установка массива байтов
                user.Image = imageData;
            }

          


            return user;
        }
      
    }
}
