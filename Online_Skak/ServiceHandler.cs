using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Online_Skak
{
    public class ServiceHandler
    {
        private Guid _clientId;
        private Service.HelloServiceClient _helloServiceClient;
        private ServiceCallback _helloServiceCallback;
        private int clientCounter;
        private bool canConnect = true;

        public ServiceHandler()
        {
            _helloServiceCallback = new ServiceCallback();
            _helloServiceCallback.ClientNotified += HelloServiceCallback_ClientNotified;
            _helloServiceClient = new Service.HelloServiceClient(new InstanceContext(_helloServiceCallback));
            _clientId = _helloServiceClient.Subscribe();
            clientCounter = _helloServiceClient.IncrementNumber();
            ConnectToGame();
        }

        private void ConnectToGame()
        {
            if (clientCounter > 2)
            {
                MessageBox.Show("Error: 2 Players are already connected");
                canConnect = false;
            }
        }

        public int IsPlayerWhiteTeam()
        {
            if(_clientId.ToString() == _helloServiceClient.GetClientID(0).ToString())
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public bool GetCanConnect()
        {
            return canConnect;
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
