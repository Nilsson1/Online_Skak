using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Online_Skak
{
    public class ServiceHandler
    {
        private Guid _clientId;
        private Service.HelloServiceClient _helloServiceClient;
        private ServiceCallback _helloServiceCallback;


        public ServiceHandler()
        {
            _helloServiceCallback = new ServiceCallback();
            _helloServiceCallback.ClientNotified += HelloServiceCallback_ClientNotified;
            _helloServiceClient = new Service.HelloServiceClient(new InstanceContext(_helloServiceCallback));
            _clientId = _helloServiceClient.Subscribe();
        }



        public void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            _helloServiceClient.BroadCast(_clientId, message);
        }

        public delegate void MessageRecievedDelegate(string message);
        public event MessageRecievedDelegate MessageRecieved;

        private void HelloServiceCallback_ClientNotified(object sender, ClientNotifiedEventArgs e)
        {
            try
            {
                if (MessageRecieved != null)
                    MessageRecieved.Invoke(e.Message);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception.
                Console.WriteLine("HelloServiceCallback_ClientNotified: " + ex + "Sender: " + sender);
            }
        }
    }
}
