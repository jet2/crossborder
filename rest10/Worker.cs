using HidSharp;
using HidSharp.Reports;
using HidSharp.Reports.Input;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rest10
{
    public class Worker : BackgroundService
    {
       
        public static HidDevice yscanner;
        // Read only instance of IHubContext
        private readonly IHubContext<MyHub> _hub;
        private HidDeviceInputReceiver inputReceiver;
        private DeviceItemInputParser inputParser;
        private DeviceItem deviceItem;
        private DeviceList list;
        HidStream hidStream;
        ReportDescriptor reportDescriptor;
        byte[] inputReportBuffer;

        Report inputReport;
        int readerState = 0;


        //public delegate bool DeviceFilter(Device device);

        // Inject IHubContext to constructor
        public Worker(IHubContext<MyHub> hub)
        {
            _hub = hub;
            Console.WriteLine("Start");
            ReaderInit();
        }

        public bool DeviceFilterDelegate(Device device)
        {
            bool result = false;
            if (device.DevicePath == @"\\?\hid#vid_046e&pid_52c3&col02#8&14b012ba&3&0001#{4d1e55b2-f16f-11cf-88cb-001111000030}") {  
                result = true;
            }
            return result;
        }

        private async void DevicePlugHandler(object sender, EventArgs e)
        {
            DeviceFilter DeviceFilterDelegateInstance = new DeviceFilter(DeviceFilterDelegate);
            var hidDeviceList = list.GetAllDevices(DeviceFilterDelegateInstance).ToArray();
           // Console.WriteLine(hidDeviceList.Count()>0?"Connected":"DisConnected");
            await handleListChanged(hidDeviceList);
        }

        private async void ReportHandler(object sender, EventArgs e) 
        {
            if (inputReceiver.TryRead(inputReportBuffer, 0, out inputReport))
            {

                // Parse the report if possible.
                // This will return false if (for example) the report applies to a different DeviceItem.
                if (inputParser.TryParseReport(inputReportBuffer, 0, inputReport))
                {
                    string finalString = Encoding.ASCII.GetString(inputReportBuffer);
                    if (inputReportBuffer[2] != 0)
                    {
                        Console.WriteLine($"Report {finalString[1..]}={finalString.Length}");
                        await _hub.Clients.All.SendAsync("readerreport", finalString[1..]);
                    }
                }
            }
        }

        private async Task handleListChanged(Device []hidDeviceList)
        {
            if (hidDeviceList.Count() > 0)
            {
                Console.WriteLine($"Connected");
                
                yscanner = (HidDevice)hidDeviceList[0];
                yscanner.Open();
                reportDescriptor = yscanner.GetReportDescriptor();
                if (yscanner.TryOpen(out hidStream))
                {
                        readerState = 1;
                  
                        inputReport = reportDescriptor.FeatureReports.FirstOrDefault();
                        inputReportBuffer = new byte[yscanner.GetMaxInputReportLength()];
                        inputParser = inputReport.DeviceItem.CreateDeviceItemInputParser();

                        inputReceiver = reportDescriptor.CreateHidDeviceInputReceiver();
                        inputReceiver.Start(hidStream);
                        inputReceiver.Received -= ReportHandler;
                        inputReceiver.Received += ReportHandler;
                }
                else { Console.WriteLine($"Still Closed"); }
            }
            else
            {
                readerState = 0;
                Console.WriteLine($"Disconnected");
            }

        }

        private async void ReaderInit()
        {
            list = DeviceList.Local;
            list.Changed += DevicePlugHandler;
            DeviceFilter DeviceFilterDelegateInstance = new DeviceFilter(DeviceFilterDelegate);
            var hidDeviceList = list.GetAllDevices(DeviceFilterDelegateInstance).ToArray();
            await handleListChanged(hidDeviceList);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hub.Clients.All.SendAsync("readerstate", readerState.ToString());
                await Task.Delay(1000);
            }
        }
    }
}
