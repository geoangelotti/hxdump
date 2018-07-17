using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace hxdump
{
	class Program
	{
		static String _ToHex(byte givenByte)
		{
			return BitConverter.ToString(new byte[] { givenByte });
		}

		static String _makeLong(String givenString)
		{
			for (int i = givenString.Length; i < 8; i ++)
			{
				givenString = "0" + givenString;
			}
			return givenString;
		}

		static void Main(string[] args)
		{
			try
			{
#if DEBUG
				Array.Resize(ref args, 1);
				args[0] = ("hxdump.exe");
#endif
				StringBuilder sb = new StringBuilder();
				byte[] bytes = File.ReadAllBytes(args[0]);
				UInt32 i = 0;
				foreach (byte item in bytes)
				{
					if (i % 16 == 0)
					{
						sb.Append(_makeLong(string.Format("{0:X}", i)));
						sb.Append(": ");
					}
					sb.Append(_ToHex(item));
					sb.Append(" ");
					if (i % 16 == 15)
					{
						sb.Append("\n");
					}
					i++;
				}
				sb.Append("\n");
				sb.Append(_makeLong(string.Format("{0:X}", i)));
				sb.Append("\n");
				Console.Write(sb.ToString());
			}
			catch (IndexOutOfRangeException)
			{
				Console.Error.WriteLine("No input given.");
			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine("File not found.");
			}
			catch (OutOfMemoryException)
			{
				Console.Error.WriteLine("Out of memory.");
			}
			catch (SystemException)
			{
				Console.Error.WriteLine("Houston we have a problem.");
			}
#if DEBUG
			finally
			{
				Console.Read();
			}
#endif
		}
	}
}
