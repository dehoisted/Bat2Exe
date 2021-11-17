using System;
using System.IO;

namespace Bat2Exe
{
	class BEncoder
	{
		public string encoded_output = "";
		protected readonly string batch_code = "", encoded_bat_file = "encoded_source.bat";
		// More encoding methods coming soon
		public enum Methods
		{
			bom = 1,
		}

        public string SortCode1(string batch_code)
		{
			string code = "";
			var sr = new StringReader(batch_code);
			string codeLine;
			while ((codeLine = sr.ReadLine()) != null)
				code = "&" + codeLine;
			return code;
        }

		public byte[] GetBOM()
		{
			byte[] data = { 0xFF, 0xFE };
			return data;
		}

		private string BomM1()
		{
			File.WriteAllBytes("Source\\" + encoded_bat_file, GetBOM());
			File.AppendAllText("Source\\" + encoded_bat_file, batch_code);
			string encoded = File.ReadAllText("Source\\" + encoded_bat_file);
			return encoded;
		}

		public BEncoder() { }
		public BEncoder(string batch_code, Methods method)
        {
			this.batch_code = batch_code;
			switch (method)
            {
				case Methods.bom:
					this.encoded_output = this.BomM1();
					break;
				default:
					throw new Exception("Invalid encoding method was chosen.");
			}
        }
	}
}