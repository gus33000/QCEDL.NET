// Copyright (c) 2018, Rene Lergner - @Heathcliff74xda
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using EDLTests.Qualcomm.EmergencyDownload.Transport;

namespace EDLTests.Qualcomm.EmergencyDownload.Sahara
{
    internal delegate void ReadyHandler();

    internal class QualcommSahara
    {
        private readonly QualcommSerial Serial;

        public QualcommSahara(QualcommSerial Serial)
        {
            Serial.EncodeCommands = false;
            Serial.DecodeResponses = false;
            this.Serial = Serial;
        }

        private static byte[] BuildCommandPacket(QualcommSaharaCommand SaharaCommand, byte[] CommandBuffer = null)
        {
            uint CommandID = (uint)SaharaCommand;
            uint CommandBufferLength = 0;
            if (CommandBuffer != null)
            {
                CommandBufferLength = (uint)CommandBuffer.Length;
            }
            uint Length = 0x8u + CommandBufferLength;

            byte[] Packet = new byte[Length];
            ByteOperations.WriteUInt32(Packet, 0x00, CommandID);
            ByteOperations.WriteUInt32(Packet, 0x04, Length);

            if (CommandBuffer != null && CommandBufferLength != 0)
            {
                Buffer.BlockCopy(CommandBuffer, 0, Packet, 0x08, CommandBuffer.Length);
            }

            return Packet;
        }

        private static byte[] BuildHelloResponsePacket(QualcommSaharaMode SaharaMode, uint ProtocolVersion = 2, uint SupportedVersion = 1, uint MaxPacketLength = 0 /* 0: Status OK */)
        {
            uint Mode = (uint)SaharaMode;

            // Hello packet:
            // xxxxxxxx = Protocol version
            // xxxxxxxx = Supported version
            // xxxxxxxx = Max packet length
            // xxxxxxxx = Expected mode
            // 6 dwords reserved space
            byte[] Hello = new byte[0x28];
            ByteOperations.WriteUInt32(Hello, 0x00, ProtocolVersion);
            ByteOperations.WriteUInt32(Hello, 0x04, SupportedVersion);
            ByteOperations.WriteUInt32(Hello, 0x08, MaxPacketLength);
            ByteOperations.WriteUInt32(Hello, 0x0C, Mode);
            ByteOperations.WriteUInt32(Hello, 0x10, 0);
            ByteOperations.WriteUInt32(Hello, 0x14, 0);
            ByteOperations.WriteUInt32(Hello, 0x18, 0);
            ByteOperations.WriteUInt32(Hello, 0x1C, 0);
            ByteOperations.WriteUInt32(Hello, 0x20, 0);
            ByteOperations.WriteUInt32(Hello, 0x24, 0);

            return BuildCommandPacket(QualcommSaharaCommand.HelloResponse, Hello);
        }

        private static byte[] BuildExecutePacket(uint RequestID)
        {
            byte[] Execute = new byte[0x04];
            ByteOperations.WriteUInt32(Execute, 0x00, RequestID);
            return BuildCommandPacket(QualcommSaharaCommand.Execute, Execute);
        }

        private static byte[] BuildExecuteDataPacket(uint RequestID)
        {
            byte[] Execute = new byte[0x04];
            ByteOperations.WriteUInt32(Execute, 0x00, RequestID);
            return BuildCommandPacket(QualcommSaharaCommand.ExecuteData, Execute);
        }

        private byte[] GetCommandVariable(QualcommSaharaExecuteCommand command)
        {
            Serial.SendData(BuildExecutePacket((uint)command));

            byte[] ReadDataRequest = Serial.GetResponse(null);
            uint ResponseID = ByteOperations.ReadUInt32(ReadDataRequest, 0);

            if (ResponseID != 0xE)
            {
                throw new BadConnectionException();
            }

            uint DataLength = ByteOperations.ReadUInt32(ReadDataRequest, 0x0C);

            Serial.SendData(BuildExecuteDataPacket((uint)command));

            return Serial.GetResponse(null, Length: (int)DataLength);
        }

        public byte[][] GetRKHs()
        {
            byte[] Response = GetCommandVariable(QualcommSaharaExecuteCommand.OemPKHashRead);

            List<byte[]> RootKeyHashes = [];

            int Size = 0x20;
            if (Response.Length % 0x30 == 0)
            {
                Size = 0x30;
            }

            for (int i = 0; i < Response.Length / Size; i++)
            {
                RootKeyHashes.Add(Response[(i * Size)..((i + 1) * Size)]);
            }

            return [.. RootKeyHashes];
        }

        public byte[] GetRKH()
        {
            byte[][] RKHs = GetRKHs();
            return RKHs[0];
        }

        public byte[] GetHWID()
        {
            byte[] Response = GetCommandVariable(QualcommSaharaExecuteCommand.MsmHWIDRead);
            return [.. Response.Reverse()];
        }

        public byte[] GetSerialNumber()
        {
            byte[] Response = GetCommandVariable(QualcommSaharaExecuteCommand.SerialNumRead);
            return [.. Response.Reverse()];
        }

        public bool SendImage(string Path)
        {
            bool Result = true;

            Console.WriteLine("Sending programmer: " + Path);

            byte[] ImageBuffer = null;
            try
            {
                byte[] Hello = Serial.GetResponse([0x01, 0x00, 0x00, 0x00]);

                // Incoming Hello packet:
                // 00000001 = Hello command id
                // xxxxxxxx = Length
                // xxxxxxxx = Protocol version
                // xxxxxxxx = Supported version
                // xxxxxxxx = Max packet length
                // xxxxxxxx = Expected mode
                // 6 dwords reserved space
                Console.WriteLine("Protocol: 0x" + ByteOperations.ReadUInt32(Hello, 0x08).ToString("X8"));
                Console.WriteLine("Supported: 0x" + ByteOperations.ReadUInt32(Hello, 0x0C).ToString("X8"));
                Console.WriteLine("MaxLength: 0x" + ByteOperations.ReadUInt32(Hello, 0x10).ToString("X8"));
                Console.WriteLine("Mode: 0x" + ByteOperations.ReadUInt32(Hello, 0x14).ToString("X8"));

                byte[] HelloResponse = BuildHelloResponsePacket(QualcommSaharaMode.ImageTXPending);
                Serial.SendData(HelloResponse);

                using FileStream FileStream = new(Path, FileMode.Open, FileAccess.Read);

                while (true)
                {
                    byte[] ReadDataRequest = Serial.GetResponse(null);

                    QualcommSaharaCommand CommandID = (QualcommSaharaCommand)ByteOperations.ReadUInt32(ReadDataRequest, 0);

                    bool Break = false;

                    switch (CommandID)
                    {
                        // 32-Bit data request
                        case QualcommSaharaCommand.ReadData:
                            {
                                uint ImageID = ByteOperations.ReadUInt32(ReadDataRequest, 0x08);
                                uint Offset = ByteOperations.ReadUInt32(ReadDataRequest, 0x0C);
                                uint Length = ByteOperations.ReadUInt32(ReadDataRequest, 0x10);

                                if (ImageBuffer == null || ImageBuffer.Length != Length)
                                {
                                    ImageBuffer = new byte[Length];
                                }

                                if (FileStream.Position != Offset)
                                {
                                    FileStream.Seek(Offset, SeekOrigin.Begin);
                                }

                                FileStream.Read(ImageBuffer, 0, (int)Length);

                                Serial.SendData(ImageBuffer);
                                break;
                            }
                        // End Transfer
                        case QualcommSaharaCommand.EndImageTX:
                            {
                                Break = true;
                                break;
                            }
                        // 64-Bit data request
                        case QualcommSaharaCommand.ReadData64Bit:
                            {
                                ulong ImageID = ByteOperations.ReadUInt64(ReadDataRequest, 0x08);
                                ulong Offset = ByteOperations.ReadUInt64(ReadDataRequest, 0x10);
                                ulong Length = ByteOperations.ReadUInt64(ReadDataRequest, 0x18);

                                if (ImageBuffer == null || ImageBuffer.Length != (uint)Length)
                                {
                                    ImageBuffer = new byte[Length];
                                }

                                if (FileStream.Position != (uint)Offset)
                                {
                                    FileStream.Seek((uint)Offset, SeekOrigin.Begin);
                                }

                                FileStream.Read(ImageBuffer, 0, (int)Length);

                                Serial.SendData(ImageBuffer);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine($"Unknown command: {CommandID.ToString("X8")}");
                                throw new BadConnectionException();
                            }
                    }

                    if (Break)
                    {
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                Result = false;
            }

            if (Result)
            {
                Console.WriteLine("Programmer loaded into phone memory");
            }

            return Result;
        }

        public bool Handshake()
        {
            bool Result = true;

            try
            {
                byte[] Hello = Serial.GetResponse([0x01, 0x00, 0x00, 0x00]);

                // Incoming Hello packet:
                // 00000001 = Hello command id
                // xxxxxxxx = Length
                // xxxxxxxx = Protocol version
                // xxxxxxxx = Supported version
                // xxxxxxxx = Max packet length
                // xxxxxxxx = Expected mode
                // 6 dwords reserved space
                Console.WriteLine("Protocol: 0x" + ByteOperations.ReadUInt32(Hello, 0x08).ToString("X8"));
                Console.WriteLine("Supported: 0x" + ByteOperations.ReadUInt32(Hello, 0x0C).ToString("X8"));
                Console.WriteLine("MaxLength: 0x" + ByteOperations.ReadUInt32(Hello, 0x10).ToString("X8"));
                Console.WriteLine("Mode: 0x" + ByteOperations.ReadUInt32(Hello, 0x14).ToString("X8"));

                byte[] HelloResponse = BuildHelloResponsePacket(QualcommSaharaMode.ImageTXPending);

                byte[] Ready = Serial.SendCommand(HelloResponse, [0x03, 0x00, 0x00, 0x00]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error happened");
                Console.WriteLine(ex.GetType().ToString());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Result = false;
            }

            return Result;
        }

        public bool CommandHandshake()
        {
            bool Result = true;

            try
            {
                byte[] Hello = Serial.GetResponse([0x01, 0x00, 0x00, 0x00]);

                // Incoming Hello packet:
                // 00000001 = Hello command id
                // xxxxxxxx = Length
                // xxxxxxxx = Protocol version
                // xxxxxxxx = Supported version
                // xxxxxxxx = Max packet length
                // xxxxxxxx = Expected mode
                // 6 dwords reserved space
                Console.WriteLine("Protocol: 0x" + ByteOperations.ReadUInt32(Hello, 0x08).ToString("X8"));
                Console.WriteLine("Supported: 0x" + ByteOperations.ReadUInt32(Hello, 0x0C).ToString("X8"));
                Console.WriteLine("MaxLength: 0x" + ByteOperations.ReadUInt32(Hello, 0x10).ToString("X8"));
                Console.WriteLine("Mode: 0x" + ByteOperations.ReadUInt32(Hello, 0x14).ToString("X8"));

                byte[] HelloResponse = BuildHelloResponsePacket(QualcommSaharaMode.Command);

                byte[] Ready = Serial.SendCommand(HelloResponse, null);

                uint ResponseID = ByteOperations.ReadUInt32(Ready, 0);

                if (ResponseID != 0xB)
                {
                    throw new BadConnectionException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error happened");
                Console.WriteLine(ex.GetType().ToString());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Result = false;
            }

            return Result;
        }

        public void ResetSahara()
        {
            Serial.SendCommand(BuildCommandPacket(QualcommSaharaCommand.Reset), [0x08, 0x00, 0x00, 0x00]);
        }

        public void SwitchMode(QualcommSaharaMode Mode)
        {
            byte[] SwitchMode = new byte[0x04];
            ByteOperations.WriteUInt32(SwitchMode, 0x00, (uint)Mode);

            byte[] SwitchModeCommand = BuildCommandPacket(QualcommSaharaCommand.SwitchMode, SwitchMode);

            byte[] ResponsePattern = null;
            switch (Mode)
            {
                case QualcommSaharaMode.ImageTXPending:
                    ResponsePattern = [0x04, 0x00, 0x00, 0x00];
                    break;
                case QualcommSaharaMode.MemoryDebug:
                    ResponsePattern = [0x09, 0x00, 0x00, 0x00];
                    break;
                case QualcommSaharaMode.Command:
                    ResponsePattern = [0x0B, 0x00, 0x00, 0x00];
                    break;
            }

            Serial.SendData(SwitchModeCommand);
        }

        public void StartProgrammer()
        {
            Console.WriteLine("Starting programmer");
            byte[] DoneCommand = BuildCommandPacket(QualcommSaharaCommand.Done);

            bool Started = false;
            int count = 0;

            do
            {
                count++;
                try
                {
                    byte[] DoneResponse = Serial.SendCommand(DoneCommand, [0x06, 0x00, 0x00, 0x00]);
                    Started = true;
                }
                catch (BadConnectionException)
                {
                    Console.WriteLine("Problem while starting programmer. Attempting again.");
                }
            } while (!Started && count < 3);

            if (count >= 3 && !Started)
            {
                Console.WriteLine("Maximum number of attempts to start the programmer exceeded.");
                throw new BadConnectionException();
            }

            Console.WriteLine("Programmer being launched on phone");
        }

        public async Task<bool> LoadProgrammer(string ProgrammerPath)
        {
            bool SendImageResult = await Task.Run(() => SendImage(ProgrammerPath));

            if (!SendImageResult)
            {
                return false;
            }

            await Task.Run(StartProgrammer);

            return true;
        }
    }
}
