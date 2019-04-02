using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Skak
{
    public class ServiceCallback : Service.IHelloServiceCallback
    {
        public delegate void ClientNotifiedEventHandler(object sender, ClientNotifiedEventArgs e);

        public event ClientNotifiedEventHandler ClientNotified;

        /// <summary>
        /// Notifies the client of the message by raising an event.
        /// </summary>
        /// <param name="message">Message from the server.</param>
        void Service.IHelloServiceCallback.HandleMessage(string message)
        {
            ClientNotified?.Invoke(this, new ClientNotifiedEventArgs(message));
        }

        public void Progress(int percentageComplete)
        {
            throw new NotImplementedException();
        }
    }

    public class ClientNotifiedEventArgs : EventArgs
    {
        private readonly string _message;

        public ClientNotifiedEventArgs(string message)
        {
            _message = message;
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
