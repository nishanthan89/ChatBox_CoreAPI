using ChatBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.DAL
{
    public interface IChatData
    {
        List<ResourceChatModel> GetAllResource();
        bool saveChatData(ReturnMessage returnMessage);

        List<ChatHistoryModel> GetChatHistory(int userId);
        List<EachChatHistoryModel> GetEachChatHistory(int senderId ,int receverId);


    }
}
