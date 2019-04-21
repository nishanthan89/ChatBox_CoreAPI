using ChatBox.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.DAL
{
    public class ChatDataAccessSP : IChatData
    {
        private readonly IConfiguration configuration;
        List<ResourceChatModel> resourceList = new List<ResourceChatModel>();
        List<ChatHistoryModel> chatHistoryList = new List<ChatHistoryModel>();
        List<EachChatHistoryModel> eachchatHistoryList = new List<EachChatHistoryModel>();



        public ChatDataAccessSP(IConfiguration config)
        {
            this.configuration = config;
        }
        ResourceChatModel resourceChatModel = new ResourceChatModel();
        ResourceViewModel resourceViewModel = new ResourceViewModel();

        //get all the resource from table
        public List<ResourceChatModel> GetAllResource()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");

                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Specifing the name of the SP
                    using (SqlCommand sqlCommand = new SqlCommand("getAllUsers", connection) { CommandType = CommandType.StoredProcedure })
                    {
        
                        //Executing the sql SP command
                        var result = sqlCommand.ExecuteReader();
                        //Iterating through the result
                        while (result.Read())
                        {
                            var data = new ResourceChatModel()
                            {
                                UserId = result.GetInt32(0),
                                UserName = result.GetString(1)
                            };
                            resourceList.Add(data);
                        }
                        //Closing the result object
                        result.Close();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resourceList; ;
        }

        //save chat method
        public bool saveChatData(ReturnMessage returnMessage)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SaveChatData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@senderId", returnMessage.senderId);
                cmd.Parameters.AddWithValue("@receiverId", returnMessage.sendTo);
                cmd.Parameters.AddWithValue("@message", returnMessage.message);
                cmd.Parameters.AddWithValue("@isread", 1);
                cmd.Parameters.AddWithValue("@date", returnMessage.date);
                SqlDataReader rd = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }

        //Get History of all chat
        public List<ChatHistoryModel> GetChatHistory(int userId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAllChatHistory_ToUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter("userid", userId);
                cmd.Parameters.Add(p1);

                SqlDataReader rd = cmd.ExecuteReader();


                if (rd.Read())
                {
                    //add chat history by list
                    while (rd.Read())
                    {
                        var data = new ChatHistoryModel()
                        {
                            user = rd.GetString(0),
                            sendTo = rd.GetString(1),
                            message= rd.GetString(2),
                            date = rd.GetString(3),
                            receiverId=rd.GetInt32(4)
                        };
                        chatHistoryList.Add(data);
                    }

                }
                else
                {
                    //return false;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return chatHistoryList;
        }

        //Get chat history of two people
        public List<EachChatHistoryModel> GetEachChatHistory(int senderId, int receverId)
        {
            List<EachChatHistoryModel> eachchatHistoryList = new List<EachChatHistoryModel>();

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");

                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    //Specifing the name of the SP
                    using (SqlCommand sqlCommand = new SqlCommand("GetAllEachChatHistory", connection) { CommandType = CommandType.StoredProcedure }) {
                        //Passing the parameters
                        SqlParameter p1 = sqlCommand.Parameters.Add("@senderid", SqlDbType.NChar);
                        p1.Value = senderId;
                        SqlParameter p2 = sqlCommand.Parameters.Add("@receiverid", SqlDbType.NChar);
                        p2.Value = receverId;

                        //Executing the sql SP command
                        var result = sqlCommand.ExecuteReader();
                        //Iterating through the result
                        while (result.Read())
                        {
                            var data = new EachChatHistoryModel()
                            {
                                user = result.GetString(0),
                                sendTo = result.GetString(1),
                                message = result.GetString(2),
                                date = result.GetString(3),
                                receiverId = result.GetInt32(4)
                            };
                            eachchatHistoryList.Add(data);
                        }
                        //Closing the result object
                        result.Close();
                    }

                    connection.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return eachchatHistoryList;
        }


        public bool AddnewUser(AddUser addUser)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("AddNewUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@senderId", addUser.Participant);
                cmd.Parameters.AddWithValue("@receiverId", addUser.ChatDate);

                SqlDataReader rd = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }


    }
}
