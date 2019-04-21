using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBox.DAL;
using ChatBox.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBox.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatDataController : ControllerBase
    {
        private readonly IChatData ichatData;
        //ResourceViewModel resourceViewModel = new ResourceViewModel();
        List<ResourceChatModel> resourceFinallist = new List<ResourceChatModel>();
        List<ChatHistoryModel> chatHistoryFinallist = new List<ChatHistoryModel>();
        List<EachChatHistoryModel> eachchatHistoryFinallist = new List<EachChatHistoryModel>();

        public ChatDataController(IChatData ichatData)
        {
            this.ichatData = ichatData;

        }

        [HttpGet]
        [Route("GetAllResource")]
        public List<ResourceChatModel> GetAllResource()
        {
            try
            {
                resourceFinallist = ichatData.GetAllResource();

            }
            catch (Exception)
            {

                throw;
            }
            return resourceFinallist;

        }

        //get all chat history to perticular user

        [HttpPost]
        [Route("getchathistory")]
        public Array GetChatHistory([FromBody]int loggedinuserid)
        {
            try
            {
                chatHistoryFinallist = ichatData.GetChatHistory(loggedinuserid);
                //chatHistoryFinallist = ichatData.GetChatHistory(loggedinuserid);
                // var result = chatHistoryFinallist.GroupBy(x => x.receiverId).Select(x => x).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return chatHistoryFinallist.ToArray();

        }
        //get each history
        [HttpPost]
        [Route("getchateachhistory")]
        public Array GetEachChatHistory(ParamEachHistoryModel paramEachHistoryModel)
        {
            try
            {
                eachchatHistoryFinallist = ichatData.GetEachChatHistory(paramEachHistoryModel.loggedinuserid, paramEachHistoryModel.receiverId);

            }
            catch (Exception)
            {
                throw;
            }
            return eachchatHistoryFinallist.ToArray();

        }

        [HttpPost]
        [Route("AddnewUser")]
        public void AddnewUser(AddUser addUser)
        {

        }




    }
}
