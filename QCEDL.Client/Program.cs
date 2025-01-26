using QCEDL.Client.USB;

namespace QCEDL.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            USBNotifier.FindEDLDevices();
        }
    }
}
