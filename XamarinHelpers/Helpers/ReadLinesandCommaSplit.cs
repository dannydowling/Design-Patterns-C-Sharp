using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinHelpers.Helpers
{
    internal class ReadLinesandCommaSplit
    {
        private static ArrayPool<byte>? _arrayPool;
        private static StringBuilder? sb { get; set; }

        private static string? searchTerm;

        public class LineParser
        {
            public static Collection<object> ParseLine(string line, string searchTerm)
            {
                var resultArray = new Collection<object>();

                if (line.StartsWith(searchTerm))
                {
                    int itemID = ParseSectionAsInt(line, 1); // equal to parts[1] - item ID
                    int firstField = ParseSectionAsInt(line, 2); // equal to parts[2] - first field
                    int secondField = ParseSectionAsInt(line, 3); // equal to parts[3] - second field
                    int thirdField = ParseSectionAsInt(line, 4); // equal to parts[4] - third field
                    decimal fourthField = ParseSectionAsDecimal(line, 5); // equal to parts[5] - fourth field

                    resultArray.Add(itemID);
                    resultArray.Add(firstField);
                    resultArray.Add(secondField);
                    resultArray.Add(thirdField);
                    resultArray.Add(fourthField);
                }
                return resultArray;
            }

            public static void ParseLine(string line, string searchTerm, int numCommas)
            {
                if (line.StartsWith(searchTerm))
                {
                    var tempBuffer = _arrayPool.Rent(numCommas);

                    try
                    {
                        var findCommasInLine = FindCommasInLine(line, tempBuffer);
                        // truncated for brevity
                    }
                    finally
                    {
                        //needed to avoid a memory leak
                        _arrayPool.Return(tempBuffer, true);
                    }
                }
            }

            private static byte[] FindCommasInLine(string line, byte[] nums)
            {
                byte counter = 0;

                for (byte index = 0; index < line.Length; index++)
                {
                    if (line[index] == ',')
                    {
                        nums[counter++] = index;
                    }
                }

                return nums;
            }

            private static void ViaRawStream(string inputFilePath)
            {
                var charPool = ArrayPool<char>.Shared;

                using (var reader = File.OpenRead(inputFilePath))
                {
                    try
                    {
                        bool endOfFile = false;
                        while (reader.CanRead)
                        {
                            sb.Clear();

                            while (endOfFile == false)
                            {
                                var readByte = reader.ReadByte();

                                // -1 means end of file
                                if (readByte == -1)
                                {
                                    endOfFile = true;
                                    break;
                                }

                                var character = (char)readByte;

                                // this means the line is about to end so we skip
                                if (character == '\r')
                                {
                                    continue;
                                }

                                // this line has ended
                                if (character == '\n')
                                {
                                    break;
                                }

                                sb.Append(character);

                                char[] rentedCharBuffer = charPool.Rent(sb.Length);

                                try
                                {
                                    for (int index = 0; index < sb.Length; index++)
                                    {
                                        rentedCharBuffer[index] = sb[index];
                                    }

                                    ParseLine(rentedCharBuffer.ToString(), searchTerm);
                                }
                                finally
                                {
                                    charPool.Return(rentedCharBuffer, true);
                                }
                            }
                        }
                    }

                    catch (Exception exception)
                    {
                        throw new Exception("File could not be parsed", exception);
                    }
                }
            }


            private static decimal ParseSectionAsDecimal(string line, int v)
            {
                for (var index = v; ;)
                {
                    sb.Append(line[index]);
                }

                return decimal.Parse(sb.ToString());
            }

            private static int ParseSectionAsInt(string line, int v)
            {
                for (var index = v; ;)
                {
                    sb.Append(line[index]);
                }

                return int.Parse(sb.ToString());
            }
        }
    }
}
