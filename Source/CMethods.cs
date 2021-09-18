using System.IO;

namespace Bat2Exe
{
    sealed class CMethods
    {
        public static string source_path = Directory.GetCurrentDirectory() + "\\Source\\Source.cs";

        public static void M1(string program_name, string batch_cmd, bool hide_console)
        {
            string newStr = @"""""";
            batch_cmd = batch_cmd.Replace("\"", newStr);

            File.WriteAllText(source_path, "using System;\n");

            string code_text1 = @"using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bat2Exe 
{
    class Program
    {
        ";
            string code_text2 = "private static string batch_cmd = @\"" + batch_cmd + "\";";
            string code_text3 = @"
        private static int randstr_length = 8;
        private static string official_bat2exe_github = ""https://github.com/dehoisted/Bat2Exe"";

            [DllImport(""kernel32.dll"", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        public static void Main()
        {
            FreeConsole();
            string batch_fname = GetPath();
            using (StreamWriter writer = new StreamWriter(batch_fname))  
            {
                ";
            string code_text4 = @"
            }

            System.IO.File.AppendAllText(batch_fname, batch_cmd);
            System.Diagnostics.ProcessStartInfo processStartInfo = new ProcessStartInfo(batch_fname);
            processStartInfo.UseShellExecute = true;
            ";

            string ex_code_text1 = "";
            switch (hide_console)
            {
                case true:
                    ex_code_text1 = "processStartInfo.CreateNoWindow = true;";
                   break;

                case false:
                    ex_code_text1 = "processStartInfo.CreateNoWindow = false;";
                    break;
            }

            string code_text5 = @"
            System.Diagnostics.Process p = new Process();
            p.StartInfo = processStartInfo;
            p.Start();
            p.WaitForExit();
            File.Delete(batch_fname);
        }

        public static string GetPath()
        {
            string path = Path.GetTempPath();
            path += GetRandomString();
            path += "".bat"";
            return path;
        }

        public static string GetRandomString()
        {
            const string chars = ""ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"";
            var random = new Random();
            return new string(System.Linq.Enumerable.Repeat(chars, randstr_length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}";
            File.AppendAllText(source_path, code_text1);
            File.AppendAllText(source_path, code_text2);
            File.AppendAllText(source_path, code_text3);
            File.AppendAllText(source_path, "writer.WriteLine(\"@echo off && title " + program_name + "\");");
            File.AppendAllText(source_path, code_text4);
            File.AppendAllText(source_path, ex_code_text1);
            File.AppendAllText(source_path, code_text5);
        }

        public static void M2(string program_name, string batch_cmd, bool hide_console)
        {
            string newStr = @"""""";
            batch_cmd = batch_cmd.Replace("\"", newStr);

            File.WriteAllText(source_path, "using System;\n");

            string code_text1 = @"using System.Diagnostics;
using System.IO;

namespace Bat2Exe
{
    class Program
    {
        ";
            string code_text2 = "private static string batch_cmd = @\"" + batch_cmd + "\";string official_bat2exe_github = \"https://github.com/dehoisted/Bat2Exe\";";
            string code_text3 = @"
        public static void Main()
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = ""cmd.exe"",
                RedirectStandardInput = true,
                ";

            string ex_code_text1 = "";
            switch (hide_console)
            {
                case true:
                    ex_code_text1 = @"CreateNoWindow = true,";
                    break;

                case false:
                    ex_code_text1 = @"CreateNoWindow = false,";
                    break;
            }
            string code_text4 = @"UseShellExecute = false
            };

            p.StartInfo = info;
            p.Start();
            var sr = new StringReader(batch_cmd);
            string line;

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        ";
                        string code_text5 = @"sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
";
            File.AppendAllText(source_path, code_text1);
            File.AppendAllText(source_path, code_text2);
            File.AppendAllText(source_path, code_text3); 
            File.AppendAllText(source_path, ex_code_text1);
            File.AppendAllText(source_path, code_text4);
            File.AppendAllText(source_path, "sw.WriteLine(\"@echo off && title " + program_name + " && cls\");\n");
            File.AppendAllText(source_path, code_text5);
        }
    }
}