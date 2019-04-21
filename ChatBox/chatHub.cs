using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBox.DAL;
using ChatBox.Model;
using Microsoft.AspNetCore.SignalR;

namespace ChatBox
{
    public class chatHub : Hub
    {

        private readonly IChatData ichatData;
        //ResourceViewModel resourceViewModel = new ResourceViewModel();
        List<ResourceChatModel> resourceFinallist = new List<ResourceChatModel>();

        public chatHub(IChatData ichatData)
        {
            this.ichatData = ichatData;

        }

        ReturnMessage returnMessage = new ReturnMessage();
        public async Task SendMessage(string user,int sendTo, string message ,DateTime date,int senderId, string receiverName)
        {
            //pass data into model
            returnMessage.date = date;
            returnMessage.user = user;
            returnMessage.message = message;
            returnMessage.sendTo = sendTo;
            returnMessage.senderId = senderId;

           //method call for  save chat data 
           ichatData.saveChatData(returnMessage);

            await Clients.All.SendAsync("SendMessage", returnMessage);//return message with real time
       
        }

        //this hub for private chat
        public async Task PrivateSendMessage(string user, int sendTo, string message, DateTime date, int senderId)
        {
            //pass data into model
            returnMessage.date = date;
            returnMessage.user = user;
            returnMessage.message = message;
            returnMessage.sendTo = sendTo;
            returnMessage.senderId = senderId;

            //method call for  save chat data 
            ichatData.saveChatData(returnMessage);

            await Clients.All.SendAsync("PrivateSendMessage", returnMessage);
            //TODO need to write code for save data to database
        }

        
    }
}
