using LAOL.Marketplase.AuthAdminRepository.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public class UserRepository : IUserRepository
    {
        static string key = "b14ca5898a4e4133bbce2ea2315a1916";
        public User GetUser(string userName, string password)
        {
            var sql = $@"SELECT user_id, first_name, last_name, email, password, role  FROM public.users WHERE email = '{userName}' AND password = '" + EncryptDecrypt.EncryptString(key, password) + "';";
            NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
            objpostgraceConn.Open();

            var cmd = new NpgsqlCommand(sql, objpostgraceConn);
            var reader = cmd.ExecuteReader();
            var item = new User();
            while (reader.Read())
            {
                item.Id = reader["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["user_id"].ToString());
     
                item.FirstName = reader["first_name"] == DBNull.Value ? "" : reader["first_name"].ToString();
                item.LastName = reader["last_name"] == DBNull.Value ? "" : reader["last_name"].ToString();
                item.Email = reader["email"] == DBNull.Value ? "" : reader["email"].ToString();
                item.Password = reader["password"] == DBNull.Value ? "" : reader["password"].ToString();
                item.Role = reader["role"] == DBNull.Value ? "" : reader["role"].ToString();
              
            }
            return item;
        }

        public bool CreateUser(UserDto user)
        {
            try
            {
             
                var password = EncryptDecrypt.EncryptString(key, user.Password);
                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();
                var sql = $@"INSERT INTO public.users(
                          email, password, role)
                       VALUES (  '{user.Email}', '{password}', 'Admin');";
                var cmd = new NpgsqlCommand(sql, objpostgraceConn);
                cmd.ExecuteNonQuery();
                objpostgraceConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                 return false;

            }
        }

    
    }
}
