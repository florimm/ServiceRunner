using System;

namespace GSM
{
    public class SMSData
    {
        public int ID { get; set; }
        public string FromPhone { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public bool Readet { get; set; }
    }
}