using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;

namespace GSM
{
    public class GSM : Runnable
    {
        private GsmCommMain comm;

        public GSM(Scheduler scheduler)
            : base(scheduler)
        {
            comm = new GsmCommMain();
            comm.MessageReceived += CommOnMessageReceived;
           
        }

        private void CommOnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            IMessageIndicationObject obj = e.IndicationObject;
            if (obj is MemoryLocation)
            {
                var loc = (MemoryLocation)obj;
                Logger.Info(string.Format("New message received in storage \"{0}\", index {1}.",loc.Storage, loc.Index));
                var message = comm.ReadMessage(loc.Index, loc.Storage);
                var deliver = message.Data as SmsDeliverPdu;
                if (deliver != null)
                {
                    //deliver.UserDataText qitu mirret shenimet e tekstit - kete duhet me parsu qe me nxjerr ID
                    var repo = new SMSRepository();//Ketu duhet me u marr me e regjistru messazhin ne DB
                    repo.Insert(new SMSData());
                    comm.DeleteMessage(loc.Index, loc.Storage);//Tani duhet me fshi nga telefoni
                    //Tani duhet me dergu mesazhet
                    
                }
                
            }
        }

        public override void Body()
        {
            if (!comm.IsOpen())
            {
                comm.Open();
            }
            comm.EnableMessageNotifications();
        }

        private string GetMessageStorage()
        {
            string storage = PhoneStorageType.Sim;
            if (storage.Length == 0)
                Logger.Fatal("Unknown message storage.", new ModuleException("Storage exception"));
            return storage;
        }
        private bool SendMessage(string message, string number)
        {
            try
            {
                var pdu = new SmsSubmitPdu(message, number);
                comm.SendMessage(pdu);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error sending SMS", ex);
                return false;
            }
        }
        private bool DeleteMessages()
        {
            string storage = GetMessageStorage();
            try
            {
                // Delete all messages from phone memory
                comm.DeleteMessages(DeleteScope.All, storage);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error deleting SMS-s", ex);
                return false;
            }
        }
        public override void OnDispose()
        {
            comm.Close();
            comm = null;
        }
    }

    public class SMTPP : Runnable
    {
        public SMTPP(Scheduler scheduler) : base(scheduler)
        {
        }

        public override void Body()
        {
            System.Console.WriteLine("eqeweq");
        }

        public override void OnDispose()
        {
            throw new NotImplementedException();
        }
    }
}
