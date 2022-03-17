using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace cardSender
{
    [ServiceContract]
    public interface IIpcClient
    {
        [OperationContract(IsOneWay = true)]
        void Send(string data);
    }

    public interface IIpcServer : IDisposable
    {
        void Start();
        void Stop();

        event EventHandler<DataReceivedEventArgs> Received;
    }

    [Serializable]
    public sealed class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(string data)
        {
            this.Data = data;
        }

        public string Data { get; private set; }
    }

    
    public class WcfClient : ClientBase<IIpcClient>, IIpcClient
    {
        public WcfClient() : base(new NetNamedPipeBinding(), new EndpointAddress(string.Format("net.pipe://localhost/{0}", typeof(IIpcClient).Name)))
        {
        }

        public void Send(string data)
        {
            try
            {
                this.Channel.Send(data);
            }
            catch 
            {

            }
        }
    }
    
}
