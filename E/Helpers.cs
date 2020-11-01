using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E
{
    public static class Helpers
    {
        public static bool IsValidTextFileAsync(string path, int scanLength = 4096)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var bufferLength = (int)Math.Min(scanLength, stream.Length);
                var buffer = new char[bufferLength];

                var bytesRead = reader.ReadBlock(buffer, 0, bufferLength);
                reader.Close();

                if (bytesRead != bufferLength)
                {
                    throw new IOException($"There was an error reading from the file {path}");
                }

                char[] allowedChars = {
                    (char)9,    // Horizontal Tab
                    (char)10,   // New Line 
                    (char)11,   // vertical Tab
                    (char)13    // Carriage Return
                };

                for (int i = 0; i < bytesRead; i++)
                {
                    var c = buffer[i];

                    if (char.IsControl(c) && !allowedChars.Contains(c))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static int MissingChars(string code, char a, char b, out char missing)
        {
            var _a = code.Length - code.Replace(a.ToString(), "").Length;
            var _b = code.Length - code.Replace(b.ToString(), "").Length;

            if (_a > _b)
            {
                missing = b;
                return _a - _b;
            }
            
            if (_a < _b)
            {
                missing = a;
                return _b - _a;
            }

            missing = (char)0;
            return 0;
        }

        public static void WriteColoredLine(string line, bool ok)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ForegroundColor = currentColor;
        }
    }
}
