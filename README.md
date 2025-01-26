# EDLTests

Sample output:

```
Found device on interface: 86e0d1e0-8089-11d0-9ce4-08003e301f73
Device path: \\?\usb#vid_05c6&pid_9008#5&3830a7f3&0&1#{86e0d1e0-8089-11d0-9ce4-08003e301f73}
Bus Name: QUSB_BULK_CID:0404_SN:3EB3EBC4
Mode: Qualcomm Emergency Download 9008
Qualcomm Emergency Download 9008 device detected
START TestProgrammer

Starting Firehose Test

Protocol: 0x00000002
Supported: 0x00000001
MaxLength: 0x00000400
Mode: 0x00000000
RKH[0]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (Qualcomm Technologies Incorporated, Testing Purposes Only (secboot_sha2_pss_subca2))
RKH[1]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (Qualcomm Technologies Incorporated, Testing Purposes Only (secboot_sha2_pss_subca2))
RKH[2]: D40EEE56F3194665574109A39267724AE7944134CD53CB767E293D3C40497955BC8A4519FF992B031FADC6355015AC87 (Qualcomm Technologies Incorporated, Testing Purposes Only (secboot_sha2_pss_subca2))
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

DEVPRG LOG: INFO: Binary build date: Aug 12 2020 @ 02:06:09
DEVPRG LOG: INFO: Binary build date: Aug 12 2020 @ 02:06:09
DEVPRG LOG: INFO: Chip serial num: 1051978692 (0x3eb3ebc4)
DEVPRG LOG: INFO: Supported Functions (15):
DEVPRG LOG: INFO: program
DEVPRG LOG: INFO: read
DEVPRG LOG: INFO: nop
DEVPRG LOG: INFO: patch
DEVPRG LOG: INFO: configure
DEVPRG LOG: INFO: setbootablestoragedrive
DEVPRG LOG: INFO: erase
DEVPRG LOG: INFO: power
DEVPRG LOG: INFO: firmwarewrite
DEVPRG LOG: INFO: getstorageinfo
DEVPRG LOG: INFO: benchmark
DEVPRG LOG: INFO: emmc
DEVPRG LOG: INFO: ufs
DEVPRG LOG: INFO: fixgpt
DEVPRG LOG: INFO: getsha256digest
DEVPRG LOG: INFO: End of supported functions 15
System.TimeoutException: The operation has timed out.
   at System.IO.Ports.SerialStream.Read(Byte[] array, Int32 offset, Int32 count, Int32 timeout)
   at System.IO.Ports.SerialStream.Read(Byte[] array, Int32 offset, Int32 count)
   at System.IO.Ports.SerialPort.Read(Byte[] buffer, Int32 offset, Int32 count)
   at Qualcomm.EmergencyDownload.Transport.QualcommSerial.GetResponse(Byte[] ResponsePattern, Int32 Length) in C:\Users\gus33\source\repos\EDLTests\QCEDL.NET\Transport\QualcommSerial.cs:line 126
Configuring
DEVPRG LOG: INFO: Calling handler for configure
DEVPRG LOG: INFO: Storage type set to value UFS
DEVPRG LOG: WARN: NAK: MaxPayloadSizeToTargetInBytes sent by host 1048576 larger than supported 16384
DEVPRG LOG: DEBUG: Calling usb_al_bulk_set_zlp_mode(TRUE), zlp is not set to 0
Getting Storage Info
DEVPRG LOG: INFO: Calling handler for getstorageinfo
DEVPRG LOG: DEBUG: Can't decode attribute slot with value
DEVPRG LOG: DEBUG: Can't decode attribute physical_partition_number with value
DEVPRG LOG: DEBUG: Can't decode attribute print_json with value
DEVPRG LOG: INFO: Device Total Logical Blocks: 0x1cb9800
DEVPRG LOG: INFO: Device Block Size in Bytes: 0x1000
DEVPRG LOG: INFO: Device Total Physical Partitions: 0x6
DEVPRG LOG: INFO: Device Manufacturer ID: 0x198
DEVPRG LOG: INFO: Device Serial Number: 0x48494241
DEVPRG LOG: INFO: {"storage_info": {"total_blocks":30119936, "block_size":4096, "page_size":4096, "num_physical":6, "manufacturer_id":408, "serial_num":1212760641, "fw_version":"300","mem_type":"UFS","prod_name":"THGAF8T0T43BAIRB0300"}}
DEVPRG LOG: INFO: UFS fInitialized: 0x1
DEVPRG LOG: INFO: UFS Current LUN Number: = 0x0
DEVPRG LOG: INFO: UFS Total Active LU: 0x6
DEVPRG LOG: INFO: UFS wManufacturerID: 0x198
DEVPRG LOG: INFO: UFS Boot Partition Enabled: 0x1
DEVPRG LOG: INFO: UFS Raw Device Capacity: = 0xee64000
DEVPRG LOG: INFO: UFS Min Block Size: 0x8
DEVPRG LOG: INFO: UFS Erase Block Size: 0x2000
DEVPRG LOG: INFO: UFS Allocation Unit Size: 0x1
DEVPRG LOG: INFO: UFS RPMB ReadWrite Size: = 0x40
DEVPRG LOG: INFO: UFS Number of Allocation Uint for This LU: 0x72e6
DEVPRG LOG: INFO: UFS Logical Block Size: 0xc
DEVPRG LOG: INFO: UFS Provisioning Type: 0x2
DEVPRG LOG: INFO: UFS LU Write Protect: 0x0
DEVPRG LOG: INFO: UFS Boot LUN ID: = 0x0
DEVPRG LOG: INFO: UFS Memory Type: 0x0
DEVPRG LOG: INFO: UFS LU Total Blocks: 0x1cb9800
DEVPRG LOG: INFO: UFS Supported Memory Types: 0x801f
DEVPRG LOG: INFO: UFS dEnhanced1MaxNAllocU: 0x7732
DEVPRG LOG: INFO: UFS wEnhanced1CapAdjFac: 0x300
DEVPRG LOG: INFO: UFS dEnhanced2MaxNAllocU: = 0x60
DEVPRG LOG: INFO: UFS wEnhanced2CapAdjFac: 0x300
DEVPRG LOG: INFO: UFS dEnhanced3MaxNAllocU: 0x0
DEVPRG LOG: INFO: UFS wEnhanced3CapAdjFac: 0x0
DEVPRG LOG: INFO: UFS dEnhanced4MaxNAllocU: 0x0
DEVPRG LOG: INFO: UFS wEnhanced4CapAdjFac: 0x0
DEVPRG LOG: INFO: UFS LUN Enable Bitmask: 0x3f
DEVPRG LOG: INFO: UFS Logical Block Count: 0x1cb9800
DEVPRG LOG: INFO: UFS bConfigDescrLock: 0x0
DEVPRG LOG: INFO: UFS iManufacturerName String Index: 0x1
DEVPRG LOG: INFO: UFS iProductName String Index: 0x2
DEVPRG LOG: INFO: UFS iSerialNumber String Index: 0x3
DEVPRG LOG: INFO: UFS iOemID String Index: 0x4
DEVPRG LOG: INFO: UFS Inquiry Command Output: TOSHIBA THGAF8T0T43BAIRB0300
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 0:
Name: ssd, Type: 2c86e742-745e-4fdd-bfd8-b6a7ac638772, ID: 8dc4ecb7-ff86-3d94-d387-1449399ea505, StartLBA: 6, EndLBA: 7
Name: persist, Type: 6c95e238-e343-4ba8-b489-8681ed22ad0b, ID: 39d67acd-7d30-05bb-9858-2f1aa690bd62, StartLBA: 8, EndLBA: 8199
Name: misc, Type: 82acc91f-357c-4a68-9c8f-689e1b1a23a1, ID: 8c411442-80d3-c8ae-00d9-d3f00628f36d, StartLBA: 8200, EndLBA: 8455
Name: keystore, Type: de7d4029-0f5b-41c8-ae7e-f6c023a02b33, ID: 9221b598-54b0-815f-ddf9-7467e6745997, StartLBA: 8456, EndLBA: 8583
Name: frp, Type: 91b72d4d-71e0-4cbf-9b8e-236381cff17a, ID: f659c797-c2cc-5219-9960-ed5424adba9d, StartLBA: 8584, EndLBA: 8711
Name: system_a, Type: 97d7b011-54da-4835-b3c4-917ad6e73d74, ID: 418a18c4-d030-0ccd-7dc7-1c260cee04d2, StartLBA: 8712, EndLBA: 795143
Name: system_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: b39b1b42-b209-c641-ba38-80266326e1a8, StartLBA: 795144, EndLBA: 1581575
Name: metadata, Type: 988a98c9-2910-4123-aaec-1cf6b1bc28f9, ID: c8439ac5-0671-413c-7857-f2c0c9a98e6f, StartLBA: 1581576, EndLBA: 1585671
Name: rawdump, Type: 66c9b323-f7fc-48b6-bf96-6f32e335a428, ID: 52e20a04-5877-fa15-3633-b59d1377370a, StartLBA: 1585672, EndLBA: 1618439
Name: userdata, Type: 1b81e7e6-f50d-419b-a739-2aeef8da3335, ID: 696babed-7e7f-a83f-b200-fe8dcc85f800, StartLBA: 1618440, EndLBA: 30119930
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 1:
Name: xbl_a, Type: dea0ba2c-cbdd-4805-b4f9-f428251c3e98, ID: 8d0e45a2-4955-9a83-1a85-970e0dc00f18, StartLBA: 6, EndLBA: 901
Name: xbl_config_a, Type: 5a325ae4-4276-b66d-0add-3494df27706a, ID: e93171e3-1992-bbe0-52a2-367a299b7d93, StartLBA: 902, EndLBA: 933
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 2:
Name: xbl_b, Type: dea0ba2c-cbdd-4805-b4f9-f428251c3e98, ID: 72e6d8ac-7ae2-dd48-ed99-d6344210aad5, StartLBA: 6, EndLBA: 901
Name: xbl_config_b, Type: 5a325ae4-4276-b66d-0add-3494df27706a, ID: 48ffe91d-3fed-29d6-93bc-fe4811e966b6, StartLBA: 902, EndLBA: 933
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 3:
Name: ALIGN_TO_128K_1, Type: fde1604b-d68b-4bd4-973d-962ae7a1ed88, ID: 1cd6b075-d012-3388-c25f-ac41b45f4d24, StartLBA: 6, EndLBA: 31
Name: cdt, Type: a19f205f-ccd8-4b6d-8f1e-2d9bc24cffb1, ID: b4142fdf-2448-50ef-c09c-34040552adae, StartLBA: 32, EndLBA: 63
Name: ddr, Type: 20a0c19c-286a-42fa-9ce7-f64c3226a794, ID: 59fd1268-e102-22de-8f2c-baa93b62dc90, StartLBA: 64, EndLBA: 319
Name: mdmddr, Type: 433ee193-1a8e-4d35-860f-ff66676af52b, ID: ee735e44-153d-15bb-f9e9-e63412cd042e, StartLBA: 320, EndLBA: 575
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 4:
Name: aop_a, Type: d69e90a5-4cab-0071-f6df-ab977f141a7f, ID: 982b1f8f-4770-041b-529b-12bddfe1c617, StartLBA: 6, EndLBA: 133
Name: tz_a, Type: a053aa7f-40b8-4b1c-ba08-2f68ac71a4f4, ID: c06b8dc2-38e5-7ac3-188d-8220d5c5641e, StartLBA: 134, EndLBA: 1157
Name: hyp_a, Type: e1a6a689-0c8d-4cc6-b4e8-55a4320fbd8a, ID: 267ce2d2-5fbb-add4-2479-e02ef7bf6f38, StartLBA: 1158, EndLBA: 1289
Name: modem_a, Type: ebd0a0a2-b9e5-4433-87c0-68b6b72699c7, ID: 910520a7-d099-e230-5497-0bbcc00fa421, StartLBA: 1290, EndLBA: 66825
Name: bluetooth_a, Type: 6cb747f1-c2ef-4092-add0-ca39f79c7af4, ID: 55128abb-2493-f41b-f758-b4cfb5923963, StartLBA: 66826, EndLBA: 67081
Name: mdtpsecapp_a, Type: ea02d680-8712-4552-a3be-e6087829c1e6, ID: fc372848-92bc-768a-db66-4f2b0f9eb1e1, StartLBA: 67082, EndLBA: 68105
Name: mdtp_a, Type: 3878408a-e263-4b67-b878-6340b35b11e3, ID: ffd80d5b-460b-a97d-a3b6-995121b49e12, StartLBA: 68106, EndLBA: 76297
Name: abl_a, Type: bd6928a1-4ce0-a038-4f3a-1495e3eddffb, ID: 846efe45-5b0e-8a5f-7048-aefbc645fa57, StartLBA: 76298, EndLBA: 76553
Name: dsp_a, Type: 7efe5010-2a1a-4a1a-b8bc-990257813512, ID: 4a8e9a79-c839-89d5-4b0a-ac61cd40a77c, StartLBA: 76554, EndLBA: 92937
Name: keymaster_a, Type: a11d2a7c-d82a-4c2f-8a01-1805240e6626, ID: b6bf8fc8-7477-9226-5f22-ead877cf9354, StartLBA: 92938, EndLBA: 93065
Name: boot_a, Type: 20117f86-e985-4357-b9ee-374bc1d8487d, ID: 372babb6-5774-fb01-6ad8-cb36bfc1b208, StartLBA: 93066, EndLBA: 117641
Name: cmnlib_a, Type: 73471795-ab54-43f9-a847-4f72ea5cbef5, ID: b3020ddc-8d7a-d2b4-7fc7-544bcfb8fcea, StartLBA: 117642, EndLBA: 117769
Name: cmnlib64_a, Type: 8ea64893-1267-4a1b-947c-7c362acaad2c, ID: fbbfc630-1b42-9a9d-1f0d-e14041622afc, StartLBA: 117770, EndLBA: 117897
Name: devcfg_a, Type: f65d4b16-343d-4e25-aafc-be99b6556a6d, ID: e7cbaae5-83ee-e16c-d3c3-8baae1a17d26, StartLBA: 117898, EndLBA: 117929
Name: qupfw_a, Type: 21d1219f-2ed1-4ab4-930a-41a16ae75f7f, ID: 890c5b63-1613-8c94-5bee-8a4eeb71d22f, StartLBA: 117930, EndLBA: 117949
Name: vendor_a, Type: 97d7b011-54da-4835-b3c4-917ad6e73d74, ID: eda03a7d-a31e-c005-3b23-fd2f5d217a4f, StartLBA: 117950, EndLBA: 380093
Name: vbmeta_a, Type: 4b7a15d6-322c-42ac-8110-88b7da0c5d77, ID: 17bcd884-c488-7c77-c143-0fd54d64f8ff, StartLBA: 380094, EndLBA: 380109
Name: dtbo_a, Type: 24d0d418-d31d-4d8d-ac2c-4d4305188450, ID: d21af434-3abd-f6f4-92b6-0ca05c56f9e1, StartLBA: 380110, EndLBA: 386253
Name: uefisecapp_a, Type: be8a7e08-1b7a-4cae-993a-d5b7fb55b3c2, ID: 3755471f-cc2e-0258-2b19-214edad8edb4, StartLBA: 386254, EndLBA: 386765
Name: imagefv_a, Type: 17911177-c9e6-4372-933c-804b678e666f, ID: 561ffae2-358b-cd28-e275-b7954b94b7ba, StartLBA: 386766, EndLBA: 387277
Name: core_nhlos_a, Type: 6690b4ce-70e9-4817-b9f1-25d64d888357, ID: 2311428e-4b2c-8100-ad45-41e6ac953776, StartLBA: 387278, EndLBA: 430797
Name: odm_a, Type: b4184eda-2088-4863-a18f-9576f717adf3, ID: 84f33fb4-36d6-1238-70eb-714a3b906b26, StartLBA: 430798, EndLBA: 447181
Name: aop_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 94445556-c4df-0e81-f734-a9c37b498cce, StartLBA: 447182, EndLBA: 447309
Name: tz_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 70b2bcc8-2225-988f-38bd-3fc0575a9a0e, StartLBA: 447310, EndLBA: 448333
Name: hyp_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 0f41b058-5da0-1ac8-eb4c-8438478afbed, StartLBA: 448334, EndLBA: 448465
Name: modem_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: e105b80d-1031-d760-1c76-791e1bd9990b, StartLBA: 448466, EndLBA: 514001
Name: bluetooth_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: dd2d4117-d01a-03a6-9dbf-0998a5b315f7, StartLBA: 514002, EndLBA: 514257
Name: mdtpsecapp_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 194d6f83-e439-5ef8-626f-574d3996505f, StartLBA: 514258, EndLBA: 515281
Name: mdtp_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: e681ad34-5e37-3b63-2a63-39541ddd7869, StartLBA: 515282, EndLBA: 523473
Name: abl_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 282a0330-2206-dece-d653-16ee9dd362a5, StartLBA: 523474, EndLBA: 523729
Name: dsp_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 2a0a6799-5f7d-ae33-926b-bda6514570b3, StartLBA: 523730, EndLBA: 540113
Name: keymaster_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: fa370681-909b-3c05-a375-53f2bbc8fc22, StartLBA: 540114, EndLBA: 540241
Name: boot_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 3d8cfc62-8e1b-1d35-c4e4-0e1c67210233, StartLBA: 540242, EndLBA: 564817
Name: cmnlib_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 8d029c03-cdf0-f723-7c3b-60bd74038359, StartLBA: 564818, EndLBA: 564945
Name: cmnlib64_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 178fe105-ffbb-b94f-7add-1509df9a5da9, StartLBA: 564946, EndLBA: 565073
Name: devcfg_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: e6ad3649-a7d5-6251-025a-3946fa103629, StartLBA: 565074, EndLBA: 565105
Name: qupfw_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 749b1b40-d71f-f70b-6ee8-8b3df02c9718, StartLBA: 565106, EndLBA: 565125
Name: vendor_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: c8100412-af03-6616-ca44-968034e5a24c, StartLBA: 565126, EndLBA: 827269
Name: vbmeta_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 2946d86b-238a-4680-7f1c-351d81936e22, StartLBA: 827270, EndLBA: 827285
Name: dtbo_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: f6eb3e71-fcf3-b1bc-3bb8-68da2cef87df, StartLBA: 827286, EndLBA: 833429
Name: uefisecapp_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 71628949-58cf-fc20-f0c2-960570e18c0c, StartLBA: 833430, EndLBA: 833941
Name: imagefv_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: b2054831-ff7c-6adb-9aa1-6895b7c6c106, StartLBA: 833942, EndLBA: 834453
Name: odm_b, Type: 77036cd4-03d5-42bb-8ed1-37e5a88baa34, ID: 1dd61800-1fc6-d046-821f-219617ee9675, StartLBA: 834454, EndLBA: 850837
Name: devinfo, Type: 65addcf4-0c5c-4d9a-ac2d-d90b5cbfcd03, ID: 1c14294f-b28d-3bb6-8acf-af179e06d8db, StartLBA: 850838, EndLBA: 850838
Name: dip, Type: 4114b077-005d-4e12-ac8c-b493bda684fb, ID: a2ab3ebe-47ea-262c-9417-d3a5493427bc, StartLBA: 850839, EndLBA: 851094
Name: apdp, Type: e6e98da2-e22a-4d12-ab33-169e7deaa507, ID: 773a3918-c501-b023-26de-ec36df86365e, StartLBA: 851095, EndLBA: 851158
Name: msadp, Type: ed9e8101-05fa-46b7-82aa-8d58770d200b, ID: c03470d5-00f1-346e-5746-2673e5fb9900, StartLBA: 851159, EndLBA: 851222
Name: spunvm, Type: e42e2b4c-33b0-429b-b1ef-d341c547022c, ID: c91b2d4d-3e7b-66f0-cc00-9bbebd4ca9b7, StartLBA: 851223, EndLBA: 853270
Name: splash, Type: ad99f201-dc71-4e30-9630-e19eef553d1b, ID: f9ac196e-298e-cc7e-c299-d223599ea653, StartLBA: 853271, EndLBA: 861626
Name: limits, Type: 10a0c19c-516a-5444-5ce3-664c3226a794, ID: dc416b35-76c6-515c-fc87-86214f794792, StartLBA: 861627, EndLBA: 861627
Name: toolsfv, Type: 97745aba-135a-44c3-9adc-05616173c24c, ID: e5233a81-6e54-b860-b3f5-7a06f7566f0f, StartLBA: 861628, EndLBA: 861883
Name: logfs, Type: bc0330eb-3410-4951-a617-03898dbe3372, ID: cabcf1ac-4db2-627b-63b1-0f2f64255b50, StartLBA: 861884, EndLBA: 863931
Name: cateloader, Type: aa9a5c4c-4f1f-7d3a-014a-22bd33bf7191, ID: db3ace3b-4f3d-40b9-35c4-1425854fecc2, StartLBA: 863932, EndLBA: 864443
Name: logdump, Type: 5af80809-aabb-4943-9168-cdfc38742598, ID: ef1a1270-754e-5f80-3b36-9c07d11a24ae, StartLBA: 864444, EndLBA: 880827
Name: storsec, Type: 02db45fe-ad1b-4cb6-aecc-0042c637defa, ID: a5bfe3bc-28a8-1526-c1cc-50875e907344, StartLBA: 880828, EndLBA: 880859
Name: multiimgoem, Type: e126a436-757e-42d0-8d19-0f362f7a62b8, ID: 40f3bbfe-7ca9-9668-0a57-7f8302b8be33, StartLBA: 880860, EndLBA: 880867
Name: multiimgqti, Type: 846c6f05-eb46-4c0a-a1a3-3648ef3f9d0e, ID: 55b0633f-f4e6-57e1-d371-ac99cb44fac8, StartLBA: 880868, EndLBA: 880875
Name: uefivarstore, Type: 165bd6bc-9250-4ac8-95a7-a93f4a440066, ID: 3a47e80f-112b-d957-8483-d11789a9a057, StartLBA: 880876, EndLBA: 881003
Name: secdata, Type: 76cfc7ef-039d-4e2c-b81e-4dd8c2cb2a93, ID: f0f11600-1d68-22f2-89e3-db445dcd89e9, StartLBA: 881004, EndLBA: 881010
Name: catefv, Type: 80c23c26-c3f9-4a19-bb38-1e457daceb09, ID: 7d4fe7d3-0304-ec8c-a020-13cc3cf62bde, StartLBA: 881011, EndLBA: 881138
Name: catecontentfv, Type: e12d830b-7f62-4f0b-b48a-8178c5bf3ac1, ID: 20b9ea87-1c8f-b5b3-e3b4-132399d5add2, StartLBA: 881139, EndLBA: 881394
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: DEBUG: Can't decode attribute skip_bad_block with value
DEVPRG LOG: DEBUG: Can't decode attribute get_spare with value
DEVPRG LOG: DEBUG: Can't decode attribute ecc_disabled with value
LUN 5:
Name: ALIGN_TO_128K_2, Type: 6891a3b7-0ccc-4705-bb53-2673cac193bd, ID: 6b711362-41ce-e0d9-e524-e4b75ab6cbd1, StartLBA: 6, EndLBA: 31
Name: modemst1, Type: ebbeadaf-22c9-e33b-8f5d-0e81686a68cb, ID: 90889e65-53f2-dacb-6eca-437e4455ecb1, StartLBA: 32, EndLBA: 543
Name: modemst2, Type: 0a288b1f-22c9-e33b-8f5d-0e81686a68cb, ID: 211df80d-e8f1-6b59-2ab9-164c91ed4d01, StartLBA: 544, EndLBA: 1055
Name: fsg, Type: 638ff8e2-22c9-e33b-8f5d-0e81686a68cb, ID: 6e400d5e-c8dd-b5fc-db1a-1c0574875d20, StartLBA: 1056, EndLBA: 1567
Name: fsc, Type: 57b90a16-22c9-e33b-8f5d-0e81686a68cb, ID: 5757f44e-4ca0-03ac-0c5e-a18c94fe2ee6, StartLBA: 1568, EndLBA: 1599
Name: mdm1m9kefs3, Type: bf64fb9c-22c9-e33b-8f5d-0e81686a68cb, ID: 202d0034-7ece-63db-9b1f-685d92bed605, StartLBA: 1600, EndLBA: 2111
Name: mdm1m9kefs1, Type: 2290be64-22c9-e33b-8f5d-0e81686a68cb, ID: 7347e602-46b8-d898-539b-240a228b63c2, StartLBA: 2112, EndLBA: 2623
Name: mdm1m9kefs2, Type: 346c26d1-22c9-e33b-8f5d-0e81686a68cb, ID: 8468d8b1-2997-68d7-90b2-f2d756bbf189, StartLBA: 2624, EndLBA: 3135
Name: mdm1m9kefsc, Type: 5cb43a64-22c9-e33b-8f5d-0e81686a68cb, ID: 5eed15ea-b14a-2ceb-124e-971489439835, StartLBA: 3136, EndLBA: 3136
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: ERROR: Failed to open the UFS Device slot 0 partition 6
DEVPRG LOG: ERROR: Failed to open the device:3 slot:0 partition:6 error:0
DEVPRG LOG: ERROR: OPEN handle NULL and no error, weird 344391484
DEVPRG LOG: ERROR: Failed to open device, type:UFS, slot:0, lun:6 error:3
Error: Raw mode not enabled
LUN 6: No GPT found
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: ERROR: Failed to open the UFS Device slot 0 partition 7
DEVPRG LOG: ERROR: Failed to open the device:3 slot:0 partition:7 error:0
DEVPRG LOG: ERROR: OPEN handle NULL and no error, weird 344391484
DEVPRG LOG: ERROR: Failed to open device, type:UFS, slot:0, lun:7 error:3
Error: Raw mode not enabled
LUN 7: No GPT found
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: ERROR: Failed to open the UFS Device slot 0 partition 8
DEVPRG LOG: ERROR: Failed to open the device:3 slot:0 partition:8 error:0
DEVPRG LOG: ERROR: OPEN handle NULL and no error, weird 344391484
DEVPRG LOG: ERROR: Failed to open device, type:UFS, slot:0, lun:8 error:3
Error: Raw mode not enabled
LUN 8: No GPT found
Read
DEVPRG LOG: INFO: Calling handler for read
DEVPRG LOG: ERROR: Failed to open the UFS Device slot 0 partition 9
DEVPRG LOG: ERROR: Failed to open the device:3 slot:0 partition:9 error:0
DEVPRG LOG: ERROR: OPEN handle NULL and no error, weird 344391484
DEVPRG LOG: ERROR: Failed to open device, type:UFS, slot:0, lun:9 error:3
Error: Raw mode not enabled
LUN 9: No GPT found
Rebooting phone
DEVPRG LOG: INFO: Calling handler for power

Emergency programmer test succeeded

END TestProgrammer
```