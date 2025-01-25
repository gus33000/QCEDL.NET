# EDLTests

Sample output:

```
Found device on interface: 86e0d1e0-8089-11d0-9ce4-08003e301f73
Device path: \\?\usb#vid_05c6&pid_9008#5&1be30cba&0&2#{86e0d1e0-8089-11d0-9ce4-08003e301f73}
Bus Name: QUSB_BULK_CID:0404_SN:3EB3EBC4
Mode: Qualcomm Emergency Download 9008
Qualcomm Emergency Download 9008 device detected
TestProgrammer
Starting Firehose Test
Protocol: 0x00000002
Supported: 0x00000001
MaxLength: 0x00000400
Mode: 0x00000000
RKH[0]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (secboot_sha2_pss_subca2)
RKH[1]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (secboot_sha2_pss_subca2)
RKH[2]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (secboot_sha2_pss_subca2)
Manufacturer ID: 0E1 (Qualcomm)
Product ID: 00A5 (SDM855)
Die Revision: 0
OEM: 0000
Model: 0000
Serial Number: 3EB3EBC4
Sending programmer: C:\Users\gus33\Documents\prog_firehose_lite.elf
Protocol: 0x00000002
Supported: 0x00000001
MaxLength: 0x00000400
Mode: 0x00000000
Programmer loaded into phone memory
Starting programmer
Programmer being launched on phone
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Binary build date: Aug 12 2020 @ 02:06:09" /></data>
INFO: Binary build date: Aug 12 2020 @ 02:06:09
------------------------
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Binary build date: Aug 12 2020 @ 02:06:09
" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Chip serial num: 1051978692 (0x3eb3ebc4)" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Supported Functions (15):" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: program" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: read" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: nop" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: patch" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: configure" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: setbootablestoragedrive" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: erase" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: power" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: firmwarewrite" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: getstorageinfo" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: benchmark" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: emmc" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: ufs" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: fixgpt" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: getsha256digest" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: End of supported functions 15" /></data>
INFO: Binary build date: Aug 12 2020 @ 02:06:09
INFO: Chip serial num: 1051978692 (0x3eb3ebc4)
INFO: Supported Functions (15):
INFO: program
INFO: read
INFO: nop
INFO: patch
INFO: configure
INFO: setbootablestoragedrive
INFO: erase
INFO: power
INFO: firmwarewrite
INFO: getstorageinfo
INFO: benchmark
INFO: emmc
INFO: ufs
INFO: fixgpt
INFO: getsha256digest
INFO: End of supported functions 15
------------------------
System.TimeoutException: The operation has timed out.
   at System.IO.Ports.SerialStream.Read(Byte[] array, Int32 offset, Int32 count, Int32 timeout)
   at System.IO.Ports.SerialStream.Read(Byte[] array, Int32 offset, Int32 count)
   at System.IO.Ports.SerialPort.Read(Byte[] buffer, Int32 offset, Int32 count)
   at EDLTests.Qualcomm.EmergencyDownload.Transport.QualcommSerial.GetResponse(Byte[] ResponsePattern, Int32 Length) in C:\Users\gus33\source\repos\EDLTests\Qualcomm\EmergencyDownload\Transport\QualcommSerial.cs:line 126
Getting Storage Info
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Calling handler for getstorageinfo" /></data>
INFO: Calling handler for getstorageinfo
------------------------
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Device Total Logical Blocks: 0x1cb9800" /></data>
INFO: Device Total Logical Blocks: 0x1cb9800
------------------------
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Device Block Size in Bytes: 0x1000" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Device Total Physical Partitions: 0x6" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Device Manufacturer ID: 0x198" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Device Serial Number: 0x48494241" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: {&quot;storage_info&quot;: {&quot;total_blocks&quot;:30119936, &quot;block_size&quot;:4096, &quot;page_size&quot;:4096, &quot;num_physical&quot;:6, &quot;manufacturer_id&quot;:408, &quot;serial_num&quot;:1212760641, &quot;fw_version&quot;:&quot;300&quot;,&quot;mem_type&quot;:&quot;UFS&quot;,&quot;prod_name&quot;:&quot;THGAF8T0T43BAIRB0300&quot;}}" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS fInitialized: 0x1" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Current LUN Number: = 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Total Active LU: 0x6" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS wManufacturerID: 0x198" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Boot Partition Enabled: 0x1" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Raw Device Capacity: = 0xee64000" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Min Block Size: 0x8" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Erase Block Size: 0x2000" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Allocation Unit Size: 0x1" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS RPMB ReadWrite Size: = 0x40" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Number of Allocation Uint for This LU: 0x72e6" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Logical Block Size: 0xc" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Provisioning Type: 0x2" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS LU Write Protect: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Boot LUN ID: = 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Memory Type: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS LU Total Blocks: 0x1cb9800" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Supported Memory Types: 0x801f" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS dEnhanced1MaxNAllocU: 0x7732" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS wEnhanced1CapAdjFac: 0x300" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS dEnhanced2MaxNAllocU: = 0x60" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS wEnhanced2CapAdjFac: 0x300" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS dEnhanced3MaxNAllocU: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS wEnhanced3CapAdjFac: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS dEnhanced4MaxNAllocU: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS wEnhanced4CapAdjFac: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS LUN Enable Bitmask: 0x3f" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Logical Block Count: 0x1cb9800" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS bConfigDescrLock: 0x0" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS iManufacturerName String Index: 0x1" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS iProductName String Index: 0x2" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS iSerialNumber String Index: 0x3" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS iOemID String Index: 0x4" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: UFS Inquiry Command Output: TOSHIBA THGAF8T0T43BAIRB0300 " /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<response value="ACK" rawmode="false" /></data>
INFO: Device Block Size in Bytes: 0x1000
INFO: Device Total Physical Partitions: 0x6
INFO: Device Manufacturer ID: 0x198
INFO: Device Serial Number: 0x48494241
INFO: {"storage_info": {"total_blocks":30119936, "block_size":4096, "page_size":4096, "num_physical":6, "manufacturer_id":408, "serial_num":1212760641, "fw_version":"300","mem_type":"UFS","prod_name":"THGAF8T0T43BAIRB0300"}}
INFO: UFS fInitialized: 0x1
INFO: UFS Current LUN Number: = 0x0
INFO: UFS Total Active LU: 0x6
INFO: UFS wManufacturerID: 0x198
INFO: UFS Boot Partition Enabled: 0x1
INFO: UFS Raw Device Capacity: = 0xee64000
INFO: UFS Min Block Size: 0x8
INFO: UFS Erase Block Size: 0x2000
INFO: UFS Allocation Unit Size: 0x1
INFO: UFS RPMB ReadWrite Size: = 0x40
INFO: UFS Number of Allocation Uint for This LU: 0x72e6
INFO: UFS Logical Block Size: 0xc
INFO: UFS Provisioning Type: 0x2
INFO: UFS LU Write Protect: 0x0
INFO: UFS Boot LUN ID: = 0x0
INFO: UFS Memory Type: 0x0
INFO: UFS LU Total Blocks: 0x1cb9800
INFO: UFS Supported Memory Types: 0x801f
INFO: UFS dEnhanced1MaxNAllocU: 0x7732
INFO: UFS wEnhanced1CapAdjFac: 0x300
INFO: UFS dEnhanced2MaxNAllocU: = 0x60
INFO: UFS wEnhanced2CapAdjFac: 0x300
INFO: UFS dEnhanced3MaxNAllocU: 0x0
INFO: UFS wEnhanced3CapAdjFac: 0x0
INFO: UFS dEnhanced4MaxNAllocU: 0x0
INFO: UFS wEnhanced4CapAdjFac: 0x0
INFO: UFS LUN Enable Bitmask: 0x3f
INFO: UFS Logical Block Count: 0x1cb9800
INFO: UFS bConfigDescrLock: 0x0
INFO: UFS iManufacturerName String Index: 0x1
INFO: UFS iProductName String Index: 0x2
INFO: UFS iSerialNumber String Index: 0x3
INFO: UFS iOemID String Index: 0x4
INFO: UFS Inquiry Command Output: TOSHIBA THGAF8T0T43BAIRB0300
<?xml version="1.0" encoding="utf-16"?><Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><response value="ACK" rawmode="false" /></Data>
------------------------
Getting Storage Info
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Calling handler for read" /></data>
INFO: Calling handler for read
------------------------
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<response value="ACK" rawmode="true" /></data>
------------------------
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<response value="ACK" rawmode="false" /></data>
<?xml version="1.0" encoding="utf-16"?><Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><response value="ACK" rawmode="false" /></Data>
------------------------
Rebooting phone
------------------------
<?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Calling handler for power" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<response value="ACK" rawmode="false" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: Will issue reset/power off 100 useconds, if this hangs check if watchdog is enabled" /></data><?xml version="1.0" encoding="UTF-8" ?>
<data>
<log value="INFO: bsp_target_reset() 0" /></data>
INFO: Calling handler for power
<?xml version="1.0" encoding="utf-16"?><Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><response value="ACK" rawmode="false" /></Data>
INFO: Will issue reset/power off 100 useconds, if this hangs check if watchdog is enabled
INFO: bsp_target_reset() 0
------------------------
Emergency programmer test succeeded
TestProgrammer
```