
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Data Source=DESKTOP-KHVC2BC;Initial Catalog=PZ_K;Integrated Security=True";
            UserDAL dal = new UserDAL(connStr);
            UserDTO currentUser = null;
            SecurityDTO security = null;

            char menuItem;
            char menuUpdateItem;


            //dal.PutImageBinaryInDb(@"G:\Project Kyrs\Kyrsova_PZ\Images\1.jpg", 4);

            while (true)
            {
                ShowMenuItems();
                menuItem = char.Parse(Console.ReadLine());

                switch (menuItem)
                {
                    case 'l':
                        foreach (UserDTO cUser in dal.GetAllUsers())
                        {
                            Console.WriteLine(cUser.GetInfo());
                        }
                        break;
                    case 'f':

                        Console.Write("Enter Id : ");
                        int iD;
                        Int32.TryParse(Console.ReadLine(), out iD);
                        currentUser = dal.GetUserById(iD);
                        Console.WriteLine(currentUser.GetInfo());

                        break;

                    case 'u':
                        
                        Console.Write("Enter Id : ");
                        int id;
                     
                        Int32.TryParse(Console.ReadLine() , out id);
                        currentUser = dal.GetUserById(id);
                        if (currentUser != null)
                        {
                            Console.WriteLine(currentUser.GetInfo());

                            ShowMenuUpdateItems();
                           
                            while (true)
                            {
                              
                                menuUpdateItem = Console.ReadLine()[0];
                                switch (menuUpdateItem)
                                {
                                    case '1':
                                        Console.Write("Enter FirstName : ");
                                        currentUser.FirstName = Console.ReadLine();
                                        break;
                                    case '2':

                                        Console.Write("Enter LastName : ");
                                        currentUser.LastName = Console.ReadLine();
                                        break;
                                    case '3':
                                        Console.Write("Enter Gender : ");
                                        currentUser.Gender = Console.ReadLine();
                                        break;
                                    case '4':
                                        Console.Write("Enter Address : ");
                                        currentUser.Address = Console.ReadLine();
                                        break;
                                    case '5':
                                        Console.Write("Enter Mail : ");
                                        currentUser.Mail = Console.ReadLine();
                                        break;
                                    case '6':
                                        Console.Write("Enter Tel : ");
                                        currentUser.Tel = Console.ReadLine();
                                        break;
                                    case '7':
                                        Console.Write("Enter BankCard : ");
                                        currentUser.BankCard = Console.ReadLine();
                                        break;


                                    case '8':
                                        Console.Write("Path to images : ");
                                        string path = Console.ReadLine();
                                        if (dal.PutImageBinaryInDb(path, id) == false)
                                        {
                                            Console.WriteLine("File not found");
                                        }
                                        break;
                                    case '9':

                                        Console.Write("Securuty Info : ");
                                        security = new SecurityDTO();
                                        Console.Write("Password :");
                                        security.Password = Console.ReadLine();
                                        Console.Write("KeyWord :");
                                        security.KeyWord = Console.ReadLine();
                                        security.IsTemporary = false;
                                        break;
                                   
                                }
                                Console.WriteLine("For save and exit press e ");

                                menuUpdateItem = Console.ReadLine()[0];
                                if (menuUpdateItem == 'e')
                                {
                                    
                                    dal.UpdateUser(currentUser);
                                    security.UserId = currentUser.Id;
                                    dal.SetPasswordForUser(security);

                                    break;
                                }
                            }
                        }
                        else
                            Console.WriteLine("User not found");

                        break;

                    case 'e':
                        return;
                }

                Console.WriteLine("Pres any key to continue");
                Console.ReadKey();


            }






            //UserDTO m = new UserDTO
            //{
            //    Name = "Petro Inkognito",
            //    Mail = "PETRO@GMAIL.COM",
            //    Gender = "Male",
            //    Address= "LVIv"
            //};

            //dal.CreateUser(m);
            //m = manager.AddMovie(m);
        }

        static void ShowUsersList()
        {

        }

        static void ShowMenuItems()
        {
            Console.Clear();
            Console.WriteLine("l: List of users");
            Console.WriteLine("f: Find user by Id");
            Console.WriteLine("u: update user info");
            Console.WriteLine("e: Exit");
        }

        static void ShowMenuUpdateItems()
        {
          
            Console.WriteLine("1: FirstName");
            Console.WriteLine("2: LastName");
            Console.WriteLine("3: Gender");
            Console.WriteLine("4: Address");
            Console.WriteLine("5: Mail");
            Console.WriteLine("6: Tel");
            Console.WriteLine("7: BankCard");
            Console.WriteLine("8: Image");
            Console.WriteLine("9: Security info");
            

        }
    }
}
