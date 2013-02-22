using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SandBox.Data
{
    public class DataUtils
    {
        public static String ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba);
        }

        public static byte[] Serialize(object @object)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, @object);
                return stream.ToArray();
            }
        }

        public static object Deserialize(byte[] binaryObject)
        {
            using (var stream = new MemoryStream(binaryObject))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
