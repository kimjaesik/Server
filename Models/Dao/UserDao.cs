using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IUserDao{
        User FindUserByLoginID(string LoginID);
        User GetUser(string LoginID);
        User InsertUser(User user);
        bool UpdateUser(User user);
    }

    public class UserDao : IUserDao
    {
        public IDB db {get;}

        public UserDao(IDB db){
            this.db = db;
        }

        public User FindUserByLoginID(string LoginID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    @"SELECT user_loginID, user_DeviceCode, user_password, user_name, user_email, user_url, user_register_Time, user_isPay ,user_limitPayDate
                     FROM tb_users WHERE user_loginID = '{0}'",
                     LoginID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.User_loginID = reader.GetString(0);
                            user.User_DeviceCode = reader.GetString(1);
                            user.User_password = reader.GetString(2);
                            user.User_name = reader.GetString(3);
                            user.User_email = reader.GetString(4);
                            user.User_url = reader.GetString(5);
                            user.User_register_Time = reader.GetDateTime(6);
                            user.User_isPay = reader.GetString(7);
                            user.User_limitPayDate = reader.GetDateTime(8);

                            return user;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
        
        public User GetUser(string LoginID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   

                 string query = String.Format(
                    @"
                    SELECT 
                    user_loginID, user_DeviceCode, user_password, 
                    user_name, user_email, user_url, 
                    user_register_Time, user_isPay
                    FROM tb_users 
                    WHERE user_loginID = '{0}' ",
                     LoginID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.User_loginID = reader.GetString(0);
                            user.User_DeviceCode = reader.GetString(1);
                            user.User_password = reader.GetString(2);
                            user.User_name = reader.GetString(3);
                            user.User_email = reader.GetString(4);
                            user.User_url = reader.GetString(5);
                            user.User_register_Time = reader.GetDateTime(6);
                            user.User_isPay = reader.GetString(7);
                            user.User_limitPayDate = reader.GetDateTime(8);

                        }
                    }
                }
                
                conn.Close();
            }
            return user;
        }

        public User InsertUser(User user){
            
       
            string query = String.Format(
                @"INSERT INTO tb_users (user_loginID, user_DeviceCode, user_password, user_name, user_email, 
                user_url ,user_register_Time,user_isPay ,user_limitPayDate) VALUES ('{0}','{1}','{2}','{3}','{4}',
                '{5}', '{6}' , '{7}' , '{8}' )",
                    user.User_loginID, user.User_DeviceCode, user.User_password,  user.User_name,  user.User_email, 
                     user.User_url,  user.User_register_Time ,  user.User_isPay,  user.User_limitPayDate);

            Console.WriteLine(query);

            using(MySqlConnection conn = db.GetConnection())
            using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
            {

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                conn.Close();
            }

        
            return user;
        }

        public bool UpdateUser(User user){
            using(MySqlConnection conn = db.GetConnection())
            {
                string query = String.Format(
                    @"
                    UPDATE tb_users SET 
                        user_password = {0}, user_name = {1}, 
                        user_email = {2}, user_url = {3}, user_isPay = {4}, user_limitPayDate = {5}
                    WHERE user_loginID = {6}
                    ",
                    user.User_password , user.User_name, user.User_email, user.User_url,
                    user.User_isPay, user.User_limitPayDate,  user.User_loginID
                     );

                Console.WriteLine(query);
                
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    
                }

                conn.Close();
            }
            return true;
        }



    }
}