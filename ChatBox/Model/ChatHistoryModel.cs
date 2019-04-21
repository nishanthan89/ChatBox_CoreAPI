using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.Model
{
    public class ChatHistoryModel
    {
        public string user { get; set; }
        public string message { get; set; }
        public string date { get; set; }
        public string sendTo { get; set; }
        public int receiverId { get; set; }

        //public List<ChatHistoryModel> samplett;
    }
   
}
