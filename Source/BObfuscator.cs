using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Bat2Exe
{
    class BObfuscator
    {
		public string ObfOutput = "";
		protected readonly string batch_code = "";
		protected readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
		protected int random_str_length = 10;

		public enum Methods
		{
			alphabet = 1,
			junk = 2,
		}
		protected string RandomStr()
		{
			VBMath.Randomize();
			string randstr = "";
			short max = 1;
			short amt = checked((short)this.random_str_length);
			for (short i = max; i <= amt; i += 1)
			{
				int start = checked((int)Math.Round(Math.Floor(unchecked(26f * VBMath.Rnd()))) + 1);
				randstr += Strings.Mid(this.alphabet, start, 1);
			}
			return randstr;
		}

		private string M1()
        {
			string code = "";
			string tempC = "";
			string randstr_1 = Conversions.ToString(this.RandomStr());
			string randstr_2 = Conversions.ToString(this.RandomStr());
			string randstr_3 = Conversions.ToString(this.RandomStr());
			string str = Conversions.ToString(this.RandomStr());
			string[] array = new string[27];
			tempC += "@echo off\r\n";
			tempC = tempC + "set " + randstr_1 + "=s\r\n";
			tempC = string.Concat(new string[]
			{
		tempC,
		"%",
		randstr_1,
		"%et ",
		randstr_2,
		"=e\r\n"
			});
			tempC = string.Concat(new string[]
			{
		tempC,
		"%",
		randstr_1,
		"%%",
		randstr_2,
		"%t ",
		randstr_3,
		"=t\r\n"
			});
			int max = 0;
			checked
			{
				do
				{
					string str2 = Strings.StrConv(Conversions.ToString(this.RandomStr()), VbStrConv.Uppercase, 0);
					array[max] = Conversions.ToString(this.RandomStr());
					tempC = string.Concat(new string[]
					{
				tempC,
				"%",
				randstr_1,
				"%%",
				randstr_2,
				"%%",
				randstr_3,
				"% ",
				array[max],
				"=",
				Strings.Mid(this.alphabet, max + 1, 1),
				"\r\n"
					});
					tempC = tempC + "goto " + str2 + "\r\n";
					tempC = Conversions.ToString(Operators.ConcatenateObject(tempC,
						Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(
							Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("%" + str + "%%", this.RandomStr()), "%%"), this.RandomStr()), 
						"% "), this.RandomStr()), "="), Strings.Mid(this.alphabet, max + 1, 1)), "\r\n")));

					tempC = tempC + ":" + str2 + "\r\n";
					max++;
				}
				while (max <= 25);
				tempC += "@echo on\r\n";
				int num2 = 1;
				int num3 = Strings.Len(this.batch_code);
				for (int i = num2; i <= num3; i++)
				{
					string left = Strings.Mid(this.batch_code, i, 1);
					if (Operators.CompareString(left, "a", false) == 0)
						code = code + "%" + array[0] + "%";
					else if (Operators.CompareString(left, "b", false) == 0)
						code = code + "%" + array[1] + "%";
					else if (Operators.CompareString(left, "c", false) == 0)
						code = code + "%" + array[2] + "%";
					else if (Operators.CompareString(left, "d", false) == 0)
						code = code + "%" + array[3] + "%";
					else if (Operators.CompareString(left, "e", false) == 0)
						code = code + "%" + array[4] + "%";
					else if (Operators.CompareString(left, "f", false) == 0)
						code = code + "%" + array[5] + "%";
					else if (Operators.CompareString(left, "g", false) == 0)
						code = code + "%" + array[6] + "%";
					else if (Operators.CompareString(left, "h", false) == 0)
						code = code + "%" + array[7] + "%";
					else if (Operators.CompareString(left, "i", false) == 0)
						code = code + "%" + array[8] + "%";
					else if (Operators.CompareString(left, "j", false) == 0)
						code = code + "%" + array[9] + "%";
					else if (Operators.CompareString(left, "k", false) == 0)
						code = code + "%" + array[10] + "%";
					else if (Operators.CompareString(left, "l", false) == 0)
						code = code + "%" + array[11] + "%";
					else if (Operators.CompareString(left, "m", false) == 0)
						code = code + "%" + array[12] + "%";
					else if (Operators.CompareString(left, "n", false) == 0)
						code = code + "%" + array[13] + "%";
					else if (Operators.CompareString(left, "o", false) == 0)
						code = code + "%" + array[14] + "%";
					else if (Operators.CompareString(left, "p", false) == 0)
						code = code + "%" + array[15] + "%";
					else if (Operators.CompareString(left, "q", false) == 0)
						code = code + "%" + array[16] + "%";
					else if (Operators.CompareString(left, "r", false) == 0)
						code = code + "%" + array[17] + "%";
					else if (Operators.CompareString(left, "s", false) == 0)
						code = code + "%" + array[18] + "%";
					else if (Operators.CompareString(left, "t", false) == 0)
						code = code + "%" + array[19] + "%";
					else if (Operators.CompareString(left, "u", false) == 0)
						code = code + "%" + array[20] + "%";
					else if (Operators.CompareString(left, "v", false) == 0)
						code = code + "%" + array[21] + "%";
					else if (Operators.CompareString(left, "w", false) == 0)
						code = code + "%" + array[22] + "%";
					else if (Operators.CompareString(left, "x", false) == 0)
						code = code + "%" + array[23] + "%";
					else if (Operators.CompareString(left, "y", false) == 0)
						code = code + "%" + array[24] + "%";
					else if (Operators.CompareString(left, "z", false) == 0)
						code = code + "%" + array[25] + "%";
					else
						code += Strings.Mid(this.batch_code, i, 1);
				}
				return tempC + code;
			}
		}

		private string M2()
		{
			string code = "";
			int tempL = this.random_str_length;
			Random random = new Random();
			checked
			{
				int max = 1;
				int len = Strings.Len(this.batch_code);
				for (int k = max; k <= len; k++)
				{
					this.random_str_length = random.Next(3, 10);
					code = Conversions.ToString(Operators.ConcatenateObject(code, Operators.AddObject(Strings.Mid(this.batch_code, k, 1), Operators.AddObject(Operators.AddObject("%", this.RandomStr()), "%"))));
				}
			}

			this.random_str_length = tempL;
			return code;
		}

		public BObfuscator() { }

		public BObfuscator(string batch_code, Methods method)
        {
			this.batch_code = batch_code;
			switch (method)
            {
				case Methods.alphabet:
					this.ObfOutput = this.M1();
					break;
				case Methods.junk:
					this.ObfOutput = this.M2();
					break;
				default:
					throw new Exception("Invalid obfuscation method was chosen.");
            }
        }
	}
}