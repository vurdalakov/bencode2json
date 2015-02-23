namespace Vurdalakov.Bencode
{
    using System;
    using System.IO;
    using System.Text;

    public class BencodeToJsonConverter
    {
        public void Convert(String fileName1, String fileName2)
        {
            Convert(new FileStream(fileName1, FileMode.Open, FileAccess.Read), new FileStream(fileName2, FileMode.Create, FileAccess.Write));
        }

        public void Convert(Stream stream1, Stream stream2)
        {
            var bencode = new Byte[stream1.Length];
            stream1.Read(bencode, 0, (Int32)stream1.Length);

            String json = Convert(bencode);

            var utf8 = Encoding.UTF8.GetBytes(json);
            stream2.Write(utf8, 0, utf8.Length);
        }

        private BinaryReader _binaryReader;
        private Decoder _decoder;
        private Encoder _encoder;

        public String Convert(Byte[] bencode)
        {
            _binaryReader = new BinaryReader(new MemoryStream(bencode, false));

            _decoder = Encoding.UTF8.GetDecoder();
            _encoder = Encoding.UTF8.GetEncoder();

            StringBuilder stringBuilder = new StringBuilder();

            while (!_binaryReader.IsEof())
            {
                var b = _binaryReader.ReadByte();

                stringBuilder.Append(ReadItem(b));
            }

            var s = stringBuilder.ToString();

            if (!s.StartsWith("{") && !s.StartsWith("["))
            {
                s = "{" + s + "}";
            }

            return s;
        }

        private String ReadItem(Byte b)
        {
            switch (b)
            {
                case 100: // d
                    return ReadDictionary();
                case 108: // l
                    return ReadList();
                case 105: // i
                    return ReadInteger();
                default:
                    _binaryReader.Skip(-1);
                    return ReadString();
            }
        }

        private String ReadDictionary()
        {
            var s = "{";
            var commaNeeded = false;

            while (true)
            {
                var b = _binaryReader.ReadByte();
                if (101 == b) // e
                {
                    return s + '}';
                }

                if (commaNeeded)
                {
                    s += ", ";
                }
                commaNeeded = true;

                _binaryReader.Skip(-1);

                var name = ReadString();

                b = _binaryReader.ReadByte();
                var value = ReadItem(b);

                s += String.Format("{0}: {1}", name, value);
            }
        }

        private String ReadList()
        {
            var s = "[";
            var commaNeeded = false;

            while (true)
            {
                var b = _binaryReader.ReadByte();
                if (101 == b) // e
                {
                    return s + ']';
                }

                if (commaNeeded)
                {
                    s += ", ";
                }
                commaNeeded = true;

                s += ReadItem(b);
            }
        }

        private String ReadInteger()
        {
            var integer = ReadNumberString();

            VerifyCharacter(101); // e

            return integer;
        }

        private String ReadString()
        {
            var length = (Int32)ReadNumber();

            VerifyCharacter(58); // :

            var bytes = _binaryReader.ReadBytes(length);

            var s = "";

            try
            {
                var chars = new Char[bytes.Length];
                var numberOfChars = _decoder.GetChars(bytes, 0, bytes.Length, chars, 0);

                VerifyString(bytes, chars, numberOfChars);

                for (var i = 0; i < numberOfChars; i++)
                {
                    switch (chars[i])
                    {
                        case '\b':
                            s += @"\b";
                            break;
                        case '\f':
                            s += @"\f";
                            break;
                        case '\n':
                            s += @"\n";
                            break;
                        case '\r':
                            s += @"\r";
                            break;
                        case '\t':
                            s += @"\t";
                            break;
                        case '/':
                            s += @"\/";
                            break;
                        case '\\':
                            s += @"\\";
                            break;
                        case '"':
                            s += @"\""";
                            break;
                        default:
                            s += chars[i];
                            break;
                    }
                }
            }
            catch
            {
                s = System.Convert.ToBase64String(bytes);
            }

            return '\"' + s + '\"';
        }

        private Int64 ReadNumber()
        {
            return Int64.Parse(ReadNumberString());
        }

        private String ReadNumberString()
        {
            var s = new StringBuilder();
            while (true)
            {
                var b = _binaryReader.ReadByte();
                if ((b < 48) || (b > 57))
                {
                    _binaryReader.Skip(-1);
                    return s.ToString();
                }

                s.Append(System.Convert.ToChar(b));
            }
        }

        private void VerifyCharacter(Byte extectedCharacter)
        {
            var actualCharacter = _binaryReader.ReadByte();
            if (actualCharacter != extectedCharacter)
            {
                throw new Exception(String.Format("'{0}' character expected", System.Convert.ToChar(extectedCharacter)));
            }
        }

        private void VerifyString(Byte[] bytes, Char[] chars, Int32 numberOfChars)
        {
            var newBytes = new Byte[bytes.Length];
            var numberOfBytes = _encoder.GetBytes(chars, 0, numberOfChars, newBytes, 0, false);

            if (numberOfBytes != bytes.Length)
            {
                throw new Exception("Wrong number of bytes");
            }

            for (var i = 0; i < numberOfBytes; i++)
            {
                if (bytes[i] != newBytes[i])
                {
                    throw new Exception("Byte arrays are not same");
                }
            }
        }
    }
}
