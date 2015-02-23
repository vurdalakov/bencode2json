namespace Vurdalakov
{
    using System;
    using System.IO;

    public static class BinaryReaderExtensions
    {
        public static Boolean IsEof(this BinaryReader binaryReader)
        {
            return binaryReader.BaseStream.Position >= binaryReader.BaseStream.Length;
        }

        public static Int64 GetLength(this BinaryReader binaryReader)
        {
            return binaryReader.BaseStream.Length;
        }

        public static Int64 GetPosition(this BinaryReader binaryReader)
        {
            return binaryReader.BaseStream.Position;
        }

        public static Int64 Seek(this BinaryReader binaryReader, Int64 offset, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            return binaryReader.BaseStream.Seek(offset, seekOrigin);
        }

        public static Int64 Skip(this BinaryReader binaryReader, Int64 offset)
        {
            return Seek(binaryReader, offset, SeekOrigin.Current);
        }

        public static Byte[] Read(this BinaryReader binaryReader, Int32 index, Int32 count)
        {
            var bytes = new Byte[count];
            binaryReader.Read(bytes, index, count);
            return bytes;
        }

        public static Boolean AreEqual(this BinaryReader binaryReader, Byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                if (binaryReader.ReadByte() != buffer[i])
                {
                    return false;
                }

            }

            return true;
        }

        public static Int16 ReadInt16BigEndian(this BinaryReader binaryReader)
        {
            Byte[] array = new Byte[2];
            binaryReader.Read(array, 0, array.Length);

            SwapBytes(array, 0, 1);

            return BitConverter.ToInt16(array, 0);
        }

        public static UInt16 ReadUInt16BigEndian(this BinaryReader binaryReader)
        {
            Byte[] array = new Byte[2];
            binaryReader.Read(array, 0, array.Length);

            SwapBytes(array, 0, 1);

            return BitConverter.ToUInt16(array, 0);
        }

        public static Int32 ReadInt32BigEndian(this BinaryReader binaryReader)
        {
            Byte[] array = new Byte[4];
            binaryReader.Read(array, 0, array.Length);

            SwapBytes(array, 0, 3);
            SwapBytes(array, 1, 2);

            return BitConverter.ToInt32(array, 0);
        }

        public static UInt32 ReadUInt32BigEndian(this BinaryReader binaryReader)
        {
            Byte[] array = new Byte[4];
            binaryReader.Read(array, 0, array.Length);

            SwapBytes(array, 0, 3);
            SwapBytes(array, 1, 2);

            return BitConverter.ToUInt32(array, 0);
        }

        private static void SwapBytes(Byte[] array, Int32 offset1, Int32 offset2)
        {
            var temp = array[offset1];
            array[offset1] = array[offset2];
            array[offset2] = temp;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}
