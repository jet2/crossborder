using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kppApp
{

    //First we have to define a delegate that acts as a signature for the
    //function that is ultimately called when the event is triggered.
    //You will notice that the second parameter is of MyEventArgs type.
    //This object will contain information about the triggered event.
    public delegate void MyEventHandler(object source, MyEventArgs e);
    public delegate void MyDataEventHandler(object source, DataRecievedEventArgs e);

    //This is a class which describes the event to the class that recieves it.
    //An EventArgs class must always derive from System.EventArgs.
    //byte[] bytes = Encoding.ASCII.GetBytes(someString)
    public class MyEventArgs : EventArgs
    {
        private string EventInfo;
        public MyEventArgs(string Text)
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }

    public class DataRecievedEventArgs : EventArgs
    {
        public readonly byte[] data;

        public DataRecievedEventArgs(byte[] data)
        {
            this.data = data;
        }
    }


    public class SignalRCover
    {
        public event MyDataEventHandler OnDataReceived;
        public event MyEventHandler OnDeviceArrived;
        public event MyEventHandler OnDeviceRemoved;
        public event MyEventHandler OnServiceUp;
        public event MyEventHandler OnServiceDown;

        HubConnection connection;
        string endpoint;
        int state_down = 0;
        int state_up = 1;
        int service_state = 0;
        int device_state = 0;

        public SignalRCover(string endpoint)
        {

            this.endpoint = endpoint;

            connection = new HubConnectionBuilder()
            .WithUrl(this.endpoint)
            .Build();

            connection.DisposeAsync().Wait();

            connection.Closed += Connection_Closed;
            connection.On<string>("readerreport", (message) => UpdateReaderReport(message));
            connection.On<string>("readerstate", (message) => UpdateReaderState(message));

        }
        private async Task Connection_Closed(Exception arg)
        {
            //throw new NotImplementedException();
            if (OnServiceDown != null)
            {
                OnServiceDown(this, new MyEventArgs(endpoint  + " Down"));
            }
            if (OnDeviceRemoved != null)
            {
                OnDeviceRemoved(this, new MyEventArgs(endpoint + " Down"));
            }
            connection.DisposeAsync().Wait();
            await Task.Delay(1000);
            await makeConnect();
        }

        private async Task makeConnect()
        {
            try
            {
                await connection.StartAsync();
                if (this.service_state != state_up)
                {
                    if (OnServiceUp != null)
                    {
                        OnServiceUp(this, new MyEventArgs(endpoint + " Up"));
                        this.service_state = state_up;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.service_state != state_down)
                {
                    if (OnDeviceRemoved != null)
                    {
                        OnDeviceRemoved(this, new MyEventArgs(endpoint + " Down"));
                        this.service_state = state_down;
                    }
                }
                await Task.Delay(1000);
                await makeConnect();
            }
        }

        private void UpdateReaderState(string message)
        {
            if (message == "1")
            {
                if (OnDeviceArrived != null)
                {
                    OnDeviceArrived(this, new MyEventArgs(endpoint + " Down"));
                }
            }
            if (message == "0")
            {
                if (OnDeviceRemoved != null)
                {
                    OnDeviceRemoved(this, new MyEventArgs(endpoint + " Down"));
                }
            }
        }

        private void UpdateReaderReport(string message)
        {
            if (OnDataReceived != null)
            {
                OnDataReceived(this, new DataRecievedEventArgs(Encoding.ASCII.GetBytes(message)));
            }
        }
    }



}
