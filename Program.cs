namespace EDLTests
{
    internal class Program
    {
        private static readonly Guid COMPortGuid = new("{86E0D1E0-8089-11D0-9CE4-08003E301F73}");
        private static readonly Guid WinUSBGuid = new("{71DE994D-8B7C-43DB-A27E-2AE7CD579A0C}");

        static void Main(string[] args)
        {
            foreach ((string, string) deviceInfo in USBExtensions.GetDeviceInfos(COMPortGuid))
            {
                GetEmergencyPathType(COMPortGuid, deviceInfo);
            }

            foreach ((string, string) deviceInfo in USBExtensions.GetDeviceInfos(WinUSBGuid))
            {
                GetEmergencyPathType(WinUSBGuid, deviceInfo);
            }
        }

        private static void OnQualcommEmergencyFlashDeviceDetected(string DevicePath)
        {
            Console.WriteLine("Qualcomm Emergency Flash 9008 device detected");

        }

        private static void OnQualcommEmergencyDownloadDeviceDetected(string DevicePath)
        {
            Console.WriteLine("Qualcomm Emergency Download 9008 device detected");

            /*File.WriteAllBytes(@"C:\Users\gus33\Documents\RM820_prg_v1.0.bin", ParseAsHexFile(@"C:\Users\gus33\Documents\RM820_prg_v1.0.hex"));
            File.WriteAllBytes(@"C:\Users\gus33\Documents\FAST8960_RM820.bin", ParseAsHexFile(@"C:\Users\gus33\Documents\FAST8960_RM820.hex"));

            return;*/

            TestCode.TestProgrammer(DevicePath, @"C:\Users\gus33\Documents\prog_firehose_lite.elf").Wait();
            //TestCode.TestProgrammer(DevicePath, @"C:\Users\gus33\Documents\MPRG8x26_fh.ede").Wait();
        }

        public static void GetEmergencyPathType(Guid Guid, (string, string) deviceInfo)
        {
            string DevicePath = deviceInfo.Item1;
            string BusName = deviceInfo.Item2;

            if (DevicePath.Contains("VID_05C6&", StringComparison.OrdinalIgnoreCase)) // Qualcomm device
            {
                if (DevicePath.Contains("&PID_9008", StringComparison.OrdinalIgnoreCase))
                {
                    if ((BusName == "QHSUSB_DLOAD") || (BusName == "QHSUSB__BULK") || (BusName.StartsWith("QUSB_BULK")))
                    {
                        Console.WriteLine($"Found device on interface: {Guid}");
                        Console.WriteLine($"Device path: {DevicePath}");
                        Console.WriteLine($"Bus Name: {BusName}");

                        if (BusName?.Length == 0)
                        {
                            Console.WriteLine("Driver does not show busname, assume mode: Qualcomm Emergency Download 9008");
                        }
                        else
                        {
                            Console.WriteLine("Mode: Qualcomm Emergency Download 9008");
                        }

                        OnQualcommEmergencyDownloadDeviceDetected(DevicePath);
                    }
                    else if (BusName == "QHSUSB_ARMPRG")
                    {
                        Console.WriteLine($"Found device on interface: {Guid}");
                        Console.WriteLine($"Device path: {DevicePath}");
                        Console.WriteLine($"Bus Name: {BusName}");

                        if (BusName?.Length == 0)
                        {
                            Console.WriteLine("Driver does not show busname, assume mode: Qualcomm Emergency Flash 9008");
                        }
                        else
                        {
                            Console.WriteLine("Mode: Qualcomm Emergency Flash 9008");
                        }

                        OnQualcommEmergencyFlashDeviceDetected(DevicePath);
                    }
                }
            }
        }
    }
}
