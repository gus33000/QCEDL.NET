using EDLTests.USB;

namespace EDLTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            USBNotifier.FindEDLDevices();
        }
    }
}
