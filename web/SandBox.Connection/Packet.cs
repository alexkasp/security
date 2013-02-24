using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace SandBox.Connection
{
    public enum PacketType : byte
    {
        INITIAL            = 0x00,
        CMD_VM_STATUS      = 0x0B,   // Получить статус виртуальной машины
        CMD_VM_START       = 0x04,   // Запустить виртуальную машину
        CMD_VM_STOP        = 0x05,   // Остановить виртуальную машину
        CMD_VM_CREATE      = 0x0A,   // Создать виртуалку на базе эталонной
        CMD_VM_DELETE      = 0x0F,   // Удалить виртуалку
        CMD_SET_TARGET     = 0x08,   // Установить цель для слежения в ИС
        CMD_LOAD_MALWARE   = 0x0C,   // Загрузить ВПО в ИС
        CMD_SET_OBJECT     = 0x10,   // Установить объект за которым будет следить система

        CMD_HIDE_AND_LOCK  = 0x03,
        CMD_LOCK_DELETE    = 0x02,
        CMD_HIDE_REGISTRY  = 0x01,
        CMD_HIDE_PROCESS   = 0x11,
        CMD_SET_SIGNATURE  = 0x12,
        CMD_SET_EXTENSION  = 0x13,
        CMD_LOAD_TRAFFIC   = 0x14,
        CMD_SET_BANDWIDTH  = 0x0E,

        CDM_LOAD_PROCESSES  = 0x17,
        CDM_LOAD_FILES      = 0x15,
        CDM_LOAD_REGS       = 0x16,

        CDM_MLWR_NETCHECK   = 0xD8,     //Отправить впо на проверку в инет

        ANS_PROCESS_LIST    = 0xD7,     //Процессы
        ANS_FILES_LIST      = 0xD4,     //Файлы
        ANS_REGS_LIST       = 0xD5,     //Реестр


        INF_RSCHID_ENVID = 0xC0,     //Пакет с информацией о паре id исследования и id среды

        ANS_VM_STATUS      = 0x0B,   // Ответ о статусе виртуальной машины
        ANS_VM_START       = 0x04,   // Ответ о запуске виртуальной машины
        ANS_VM_STOP        = 0x05,   // Ответ о остановке виртуальной машины
        ANS_VM_CREATE      = 0x0A,   // Ответ о создании виртуалки
        ANS_VM_DELETE      = 0x0F,   // Ответ о удалении виртуалки
        ANS_SET_TARGET     = 0x08,   // Ответ о устанвке цели для слежения
        ANS_LOAD_MALWARE   = 0x0C,   // Ответ о загрузке ВПО в ИС
        ANS_SET_OBJECT     = 0x10,   // Ответ об установке объекта
        ANS_REPORT         = 0xE6,   // Отчет о событии
        ANS_VM_READY       = 0xE7,   // Отчет о готовности ИС к работе
        ANS_VM_COMPLETE    = 0xE8,   // Отчет о завершении работы ИС
        ANS_LOAD_TRAFFIC   = 0x14,    // Отчет о готовности к загрузке траффика
        ANS_VM_NEWCREATE = 220, //Отчёт о создании виртуалки
        ANS_VM_STOPED_BY_EVENT = 221//Виртуалка остановлена по событию
    }

    public enum PacketDirection : byte
    {
        INITIAL  = 0x00,
        REQUEST  = 0x01,
        RESPONSE = 0x02,
    }

    public class Packet
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct PacketItem
        {
            public byte Type;
            public byte Direction;
            public byte Res_1;
            public byte Res_2;
            public Guid Guid;
            public int DataLength;
            public byte[] Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Parameter
        {
            public int Length;
            public byte[] Data;
        }

        private PacketItem _packet;
        private readonly List<Parameter> _parameters;

        public Packet()
        {
            _packet = new PacketItem
            {
                Type = 0x00,
                Direction = 0x00,
                Res_1 = 0x00,
                Res_2 = 0x00,
                Guid = Guid.NewGuid(),
                DataLength = 0,
                Data = new byte[0]
            };
            _parameters = new List<Parameter>();
        }

        public PacketType Type
        {
            get { return (PacketType)_packet.Type; }
            set { _packet.Type = (byte)value; }
        }

        public PacketDirection Direction
        {
            get { return (PacketDirection)_packet.Direction; }
            set { _packet.Direction = (byte)value; }
        }

        public Guid Id
        {
            get { return _packet.Guid; }
            set { _packet.Guid = value; }
        }

        public Int32 DataLength
        {
            get { return _packet.DataLength; }
            set { _packet.DataLength = value; }
        }

        public byte[] Data
        {
            get { return _packet.Data; }
            set { _packet.Data = value; }
        }


        public void AddParameter(byte[] parameter)
        {
            Parameter par = new Parameter { Length = parameter.Length, Data = parameter };

            _parameters.Add(par);
            int parametersSize = _parameters.Select(messageParameter => 4 + messageParameter.Data.Length).Aggregate(0, (current, size) => current + size);
            _packet.DataLength = parametersSize;

            using (MemoryStream ms = new MemoryStream(parametersSize))
            {
                BinaryWriter bw = new BinaryWriter(ms);
                foreach (var messageParameter in _parameters)
                {
                    bw.Write(ParameterToByteArray(messageParameter));
                    ms.Flush();
                }
                ms.Position = 0;
                _packet.Data = ms.ToArray();
            }
        }

        public List<byte[]> GetParameters()
        {
            return _parameters.Select(parameter => parameter.Data).ToList();
        }

        private static byte[] ParameterToByteArray(Parameter parameter)
        {
            int size = 4 + parameter.Data.Length;
            using (MemoryStream ms = new MemoryStream(size))
            {
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(parameter.Length);
                if (parameter.Length > 0)
                {
                    bw.Write(parameter.Data);
                }
                ms.Flush();
                ms.Position = 0;
                return ms.ToArray();
            }
        }


        public byte[] ToByteArray()
        {
            int size = 24 + _packet.Data.Length + 1;
            using (MemoryStream ms = new MemoryStream(size))
            {
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(_packet.Type);
                bw.Write(_packet.Direction);
                bw.Write(_packet.Res_1);
                bw.Write(_packet.Res_2);
                bw.Write(_packet.Guid.ToByteArray());
                bw.Write(_packet.DataLength);
                if (_packet.DataLength > 0)
                {
                    bw.Write(_packet.Data);
                }
                bw.Write((byte)0x00);
                ms.Flush();
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        public String ToHexString()
        {
            return BitConverter.ToString(ToByteArray());
        }

        #region STATIC

        public static byte[] ToByteArray(Packet message)
        {
            return message.ToByteArray();
        }

        public static Packet ToPacket(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryReader br = new BinaryReader(ms);
                Packet message = new Packet
                                      {
                                          Type = (PacketType) br.ReadByte(),
                                          Direction = (PacketDirection) br.ReadByte()
                                      };
                br.ReadBytes(2);
                message.Id = new Guid(br.ReadBytes(16));
                Int32 dataLength = br.ReadInt32();
                byte[] paramsData = br.ReadBytes(dataLength);

                if (dataLength > 0)
                {
                    using (MemoryStream msParams = new MemoryStream(paramsData))
                    {
                        BinaryReader brParams = new BinaryReader(msParams);

                        while (brParams.PeekChar() != -1)
                        {
                            byte[] par = ReadParameter(brParams);
                            message.AddParameter(par);
                        }
                    }
                }

                return message;
            }
        }

        private static byte[] ReadParameter(BinaryReader br)
        {
            Int32 size = br.ReadInt32();
            return br.ReadBytes(size);
        }

        #endregion


    }//end class Message
}//end namespace
