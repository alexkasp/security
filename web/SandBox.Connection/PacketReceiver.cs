using System;
using System.Collections.Generic;
using System.IO;

namespace SandBox.Connection
{
    public class PacketReceiver
    {
        public static List<Byte[]> GetPackets(Byte[] buffer, Int32 maxBufferSize)
        {
            List<Byte[]> packets = new List<Byte[]>();
            Int32 bufferSize = buffer.Length;

            if (bufferSize < maxBufferSize) //все пакеты уместились в буффер
            {
                MemoryStream ms = new MemoryStream(buffer) { Position = 0 };
                
                    while ((bufferSize - ms.Position) >= 24)
                    {
                        ReadPacket(ms, ref packets); 
                    }
            }

            return packets;
        }

        private static void ReadPacket(Stream stream, ref List<byte[]> packetList)
        {
            BinaryReader reader = new BinaryReader(stream);
            Int32 beginPosition = (Int32)stream.Position;
                                reader.ReadBytes(20);
            Int32 dataLength =  reader.ReadInt32();
                                reader.ReadBytes(dataLength);
            while (reader.PeekChar() == 0)
            {
                                reader.ReadByte();
            }
            Int32 endPosition = (Int32)stream.Position;
            stream.Position = beginPosition;
            packetList.Add(reader.ReadBytes(endPosition - beginPosition));

            return;
        }

       
    }//end PacketReceiver class
}//end namespace
