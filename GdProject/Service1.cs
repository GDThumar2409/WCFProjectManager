using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GdProject.Models;

namespace GdProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        public User GetUser(string userid)
        {
            User u = new User();
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * From Users Where UserId=@UserId";
                cmd.Parameters.AddWithValue("@UserId", userid);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    u.UserId = reader["UserId"].ToString();
                    u.UserName = reader["UserName"].ToString();
                    u.Password = reader["Password"].ToString();
                }

                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }

            return u;

                
        }
        

        public void PostUser(User user)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into Users(UserId,UserName,Password) values (@UserId,@UserName,@Password);";
                cmd.Parameters.AddWithValue("@UserId",user.UserId);
                cmd.Parameters.AddWithValue("@UserName",user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                
                con.Open();
               cmd.ExecuteNonQuery();
               //cmd.ExecuteScalar();

                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }

        }

        public int PostGroup(Group grp)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into Groups(Name,AdminId) values (@Name,@AdminId);SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@Name", grp.Name);
                cmd.Parameters.AddWithValue("@AdminId", grp.AdminId);

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd.ExecuteScalar();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                return i;
            }

        }

        public void AddToGroup(int grpid, string userid)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into GroupUsers(UserId,GroupId) values (@UserId,@GroupId)";
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@GroupId", grpid);

                con.Open();
                cmd.ExecuteNonQuery();
                //cmd.ExecuteScalar();
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        public List<GroupUser> GetAllUserGroups(string userid)
        {
            List<GroupUser> l = new List<GroupUser>();
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * From GroupUsers Where UserId=@UserId";
                cmd.Parameters.AddWithValue("@UserId", userid);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                GroupUser grpusr;
                while (reader.Read())
                {
                    grpusr = new GroupUser(); 
                    grpusr.UserId = reader["UserId"].ToString();
                    grpusr.GroupId = Convert.ToInt32(reader["GroupId"]);
                    l.Add(grpusr);
                    
                }

                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
            return l;
        }

        public List<String> GetSameGroupUser(int GroupId)
        {
            List<string> l = new List<string>();
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * From GroupUsers Where GroupId=@GroupId";
                cmd.Parameters.AddWithValue("@GroupId", GroupId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                GroupUser grpusr = new GroupUser();
                while (reader.Read())
                {
                    l.Add(reader["UserId"].ToString());

                }

                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
            return l;
        }

        public void DeleteFromGroup(int grpid, string userid)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Delete From GroupUsers Where UserId=@UserId and GroupId=@GroupId";
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@GroupId", grpid);

                con.Open();
                cmd.ExecuteNonQuery();
                //cmd.ExecuteScalar();
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        public List<int> GetAllAdminGroup(string userid)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\GdProject\GdProject\APP_DATA\DatabaseGd.mdf;Integrated Security=True;Connect Timeout=30";
            List<int> l = new List<int>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * From Groups Where AdminId=@UserId";
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                GroupUser grpusr = new GroupUser();
                while (reader.Read())
                {
                    l.Add(Convert.ToInt32(reader["GroupId"]));

                }
                //cmd.ExecuteScalar();
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                return l;
            }
        }
    }
}
