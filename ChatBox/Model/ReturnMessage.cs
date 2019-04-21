using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.Model
{
    public class ReturnMessage
    {
        public string user { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public int sendTo { get; set; }
        public int senderId { get; set; }
        


    }
}
