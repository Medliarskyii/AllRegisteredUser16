using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace DAL
{
    public class UserDAL : IUserDAL
    {
        private string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserDTO CreateUser(UserDTO User)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = String.Format("insert into Users (FirstName,LastName, Gender, Address,RoleId,Mail,Tel,BankCard) output INSERTED.Id values ('{0}','{1}', '{2}', '{3}',{4},'{5}','{6}','{7}')",


                User.FirstName,
                User.LastName,
                User.Gender,
                User.Address,
                User.RoleId.ToString(),
                User.Mail,
                User.Tel,
                User.BankCard);

               

                //comm.CommandText = "insert into Users (FirstName,LastName, Gender, Address,RoleId,Mail,Tel,BankCard) output INSERTED.Id values (@FirstName,@LastName, @Gender, @Address,@RoleId,@Mail,@Tel,@BankCard)";
                //comm.Parameters.Clear();
                //comm.Parameters.AddWithValue("@FirstName", User.FirstName);
                //comm.Parameters.AddWithValue("@LastName", User.LastName);
                //comm.Parameters.AddWithValue("@Gender", User.Gender);
                //comm.Parameters.AddWithValue("@Address", User.Address);
                //comm.Parameters.AddWithValue("@RoleId", User.RoleId);
                //comm.Parameters.AddWithValue("@Mail", User.Mail);
                //comm.Parameters.AddWithValue("@Tel", User.Tel);
                //comm.Parameters.AddWithValue("@BankCard", User.BankCard);

                //comm.Parameters.AddWithValue("@BankCard", User.BankCard);
                conn.Open();


                User.Id = Convert.ToInt32(comm.ExecuteScalar());
            }


            return User;
        }

        public List<UserDTO> GetAllUsers()
        {
            UserDTO currentUser;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Users";
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> Users = new List<UserDTO>();
                while (reader.Read())
                {
                    currentUser = new UserDTO();
                    int id = 0;
                    Int32.TryParse(reader["Id"].ToString(), out id);
                    currentUser.Id = id;
                    currentUser.FirstName = reader["FirstName"].ToString();
                    currentUser.LastName = reader["LastName"].ToString();
                    currentUser.Gender = reader["Gender"].ToString();
                    currentUser.Address = reader["Address"].ToString();



                    currentUser.Mail = reader["Mail"].ToString();
                    currentUser.Tel = reader["Tel"].ToString();
                    currentUser.BankCard = reader["BankCard"].ToString();

                    Users.Add(currentUser);

                }

                return Users;
            }
        }


        public bool ChekPassword(UserDTO user, string password)
        {

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())

            {
                comm.CommandText = String.Format("SELECT [Password] FROM [Security] Where UserId = {0} ", user.Id);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SecurityDTO sec = new SecurityDTO(); ;
                if (reader.Read())
                {
                    sec.Password = reader["Password"].ToString();
                }
                if (sec.Password == password)
                    return true;
            }

            return false;
        }


        public UserDTO GetUserById(int id)
        {

            UserDTO currentUser = null;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())

            {
                comm.CommandText = "select * from Users where Id = " + id.ToString();
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {

                    currentUser = new UserDTO();
                    currentUser.Id = id;
                    currentUser.FirstName = reader["FirstName"].ToString();
                    currentUser.LastName = reader["LastName"].ToString();
                    currentUser.Gender = reader["Gender"].ToString();
                    currentUser.Address = reader["Address"].ToString();

                    currentUser.Mail = reader["Mail"].ToString();
                    currentUser.Tel = reader["Tel"].ToString();
                    currentUser.BankCard = reader["BankCard"].ToString();
                }

                return currentUser;
            }
        }

        public UserDTO GetUserByEmail(string email)
        {

            UserDTO currentUser = null;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())

            {
                comm.CommandText = String.Format("select * from Users where Mail = '{0}'" , email);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {

                    currentUser = new UserDTO();
                    currentUser.Id = Convert.ToInt32(reader["Id"].ToString());
                    currentUser.FirstName = reader["FirstName"].ToString();
                    currentUser.LastName = reader["LastName"].ToString();
                    currentUser.Gender = reader["Gender"].ToString();
                    currentUser.Address = reader["Address"].ToString();

                    currentUser.Mail = reader["Mail"].ToString();
                    currentUser.Tel = reader["Tel"].ToString();
                    currentUser.BankCard = reader["BankCard"].ToString();
                }

                return currentUser;
            }
        }



        public UserDTO UpdateUser(UserDTO User)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "UPDATE  Users SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Address = @Address, Mail = @Mail, Tel = @Tel, BankCard = @BankCard " + " WHERE Id = @Id ";
                comm.Parameters.Clear();

                comm.Parameters.AddWithValue("@Id", User.Id);
                comm.Parameters.AddWithValue("@FirstName", User.FirstName);
                comm.Parameters.AddWithValue("@LastName", User.LastName);
                comm.Parameters.AddWithValue("@Gender", User.Gender);
                comm.Parameters.AddWithValue("@Address", User.Address);
                //comm.Parameters.AddWithValue("@RoleId", User.RoleId);
                comm.Parameters.AddWithValue("@Mail", User.Mail);
                comm.Parameters.AddWithValue("@Tel", User.Tel);
                comm.Parameters.AddWithValue("@BankCard", User.BankCard);

                conn.Open();

                Convert.ToInt32(comm.ExecuteScalar());

                return User;
            }



        }


        public bool PutImageBinaryInDb(string iFile, int userId)
        {
            if (File.Exists(iFile))
            {
                SqlConnection conn = new SqlConnection(this.connectionString);

                byte[] imageData = null;
                FileInfo fInfo = new FileInfo(iFile);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(iFile, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageData = br.ReadBytes((int)numBytes);

                string iImageExtension = (Path.GetExtension(iFile)).Replace(".", "").ToLower();

                string commandText = "INSERT INTO [UsersImages] (UserId, Image, Format) VALUES(@UserId, @Image, @Format)";
                SqlCommand command = new SqlCommand(commandText, conn);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Image", (object)imageData);
                command.Parameters.AddWithValue("@Format", iImageExtension);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            return false;
        }

        public Image GetImageBinaryFromDb(string userId)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlDataAdapter dAdapter = new SqlDataAdapter(new SqlCommand("SELECT Image FROM [UsersImages] where UserId =  " + userId, conn));
            DataSet dSet = new DataSet();
            dAdapter.Fill(dSet);

            if (dSet.Tables[0].Rows.Count == 1)
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(dSet.Tables[0].Rows[0]["Image"]);
                MemoryStream mem = new MemoryStream(data);
                return Image.FromStream(mem);
            }
            return null;
        }


        public SecurityDTO GetSecurityForUser(UserDTO userDTO)/// + add loginName +///
        {
            SecurityDTO res = null;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {

                comm.CommandText = String.Format("Select * from Security where [UserId] = {0}", userDTO.Id);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {

                    res = new SecurityDTO();
                    res.Id = Convert.ToInt32(reader["Id"].ToString());
                    res.Password = reader["Password"].ToString();
                    res.KeyWord = reader["KeyWord"].ToString();
                    res.IsTemporary = Convert.ToBoolean(reader["IsTemporary"].ToString());
                    res.ExpiredDate = Convert.ToDateTime(reader["ExpiredDate"].ToString());
                   
                }

                return res;
            }
        }

        public SecurityDTO SetPasswordForUser(SecurityDTO s)/// + add loginName +///
        {

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {

                comm.CommandText = "insert into Security (UserId,Password, KeyWord, IsTemporary,ExpiredDate) output INSERTED.Id values (@UserId,@Password, @KeyWord, @IsTemporary,@ExpiredDate)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@Id", s.Id);
                comm.Parameters.AddWithValue("@UserId", s.UserId);
                comm.Parameters.AddWithValue("@Password", s.Password);
                comm.Parameters.AddWithValue("@KeyWord", s.KeyWord);
                comm.Parameters.AddWithValue("@IsTemporary", s.IsTemporary);
                if (s.IsTemporary == false)
                {
                    s.ExpiredDate = DateTime.Now;
                }
                comm.Parameters.AddWithValue("@ExpiredDate", s.ExpiredDate);
                conn.Open();


                s.Id = Convert.ToInt32(comm.ExecuteScalar());

            }
            return s;
        }


        public SecurityDTO UpdatePasswordForUser(SecurityDTO s)
        {

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {

                


                comm.CommandText = "UPDATE[dbo].[Security]  SET [UserId] = @UserId ,[Password] = @Password ,[KeyWord] = @KeyWord ,[IsTemporary] = @IsTemporary ,[ExpiredDate] = @ExpiredDate  WHERE UserId = @UserId";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@Id", s.Id);
                comm.Parameters.AddWithValue("@UserId", s.UserId);
                comm.Parameters.AddWithValue("@Password", s.Password);
                comm.Parameters.AddWithValue("@KeyWord", s.KeyWord);
                comm.Parameters.AddWithValue("@IsTemporary", s.IsTemporary);
                if (s.IsTemporary == false)
                {
                    s.ExpiredDate = DateTime.Now;
                }
                comm.Parameters.AddWithValue("@ExpiredDate", s.ExpiredDate);
                conn.Open();


                s.Id = Convert.ToInt32(comm.ExecuteScalar());

            }
            return s;
        }

    }
}

