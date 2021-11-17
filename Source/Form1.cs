using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace Bat2Exe
{
    public partial class Bat2Exe : Form
    {
        public const string github_link = "https://github.com/dehoisted", my_telegram = "https://t.me/Constex", updates_link = "https://raw.githubusercontent.com/dehoisted/Bat2Exe/main/version.txt",
            program_version = "2.1", last_updated = "11/17/2021", obf_bat_file = "obfuscated_source.bat";
        private string batch_cmd, batch_filepath = "", icon_filepath = "", OutputP, SourceP, LogP, update_str;
        private bool bat_file_entered = false;

        [Obsolete]
        public Bat2Exe()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void Bat2Exe_Load(object sender, EventArgs e)
        {
            OutputP = Directory.GetCurrentDirectory() + "\\Output";
            switch (Directory.Exists(OutputP))
            {
                case false: Directory.CreateDirectory("Output"); break;
            }
            OutputP += "\\";

            SourceP = Directory.GetCurrentDirectory() + "\\Source";
            switch (Directory.Exists(SourceP))
            {
                case false: Directory.CreateDirectory("Source"); break;
            }

            LogP = Directory.GetCurrentDirectory() + "\\Log";
            switch (Directory.Exists(LogP))
            {
                case false: Directory.CreateDirectory("Log"); break;
            }
            LogP += "\\log.txt";
        }

        public void ClearListLog()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("-- Compile Log --");
            listBox1.Items.Add("");
        }

        public string UpdateResponse()
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00); //Tls, Tls11, Tls12
                WebRequest request = WebRequest.Create(updates_link);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    response.Close();
                    this.update_str = responseFromServer;
                    return responseFromServer;
                }
            }

            catch (Exception ex)
            {
                var time = DateTime.Now;
                File.AppendAllText(LogP, time.ToString("F") + ": Updates exception thrown, message: " + ex.Message + "\n");
                MessageBox.Show("Exception message: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "error";
            }
        }

        public static void FileWarning()
        {
            MessageBox.Show("No files were selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(github_link + "/Bat2Exe/tree/main/Source");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(github_link + "/Bat2Exe/releases");
        }

        /* Main Options */

        // ICO File Button
        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox4.Checked)
            {
                openFileDialog2.Title = "Select Icon File";
                openFileDialog2.Filter = "Icon Files (*.ico)|*.ico";
                openFileDialog2.ShowDialog();

                switch (DialogResult)
                {
                    case DialogResult.Cancel:
                        guna2CheckBox4.Checked = false;
                        FileWarning();
                        break;

                    default:
                        if (icon_filepath == "")
                        {
                            FileWarning();
                            guna2CheckBox4.Checked = false;
                        }
                        else MessageBox.Show("Selected as icon file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

        // ICO File Dialog
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            icon_filepath = openFileDialog2.FileName;
        }

        // Conversion Methods
        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox3.Checked && guna2CheckBox2.Checked)
            {
                MessageBox.Show("Both conversion methods cannot be picked, choose one!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2CheckBox3.Checked = false;
            }
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox3.Checked && guna2CheckBox2.Checked)
            {
                MessageBox.Show("Both conversion methods cannot be picked, choose one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2CheckBox3.Checked = false;
            }
        }

        // Batch File Button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Batch File";
            openFileDialog1.Filter = "Batch Files (*.bat)|*.bat";
            openFileDialog1.ShowDialog();

            switch (DialogResult)
            {
                case DialogResult.Cancel:
                    FileWarning();
                    break;

                default:
                    if (batch_filepath == "")
                        FileWarning();

                    else
                    {
                        MessageBox.Show("Selected batch file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listBox1.Items.Add("Selected batch file: " + batch_filepath);

                        if (guna2CheckBox4.Checked)
                            listBox1.Items.Add("Selected icon file: " + icon_filepath);
                    }
                    break;
            }
        }

        // Batch File Dialog
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            batch_filepath = openFileDialog1.FileName;
            bat_file_entered = true;
        }

        // Label Events
        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Compression can work as a method of obfuscation.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Obfuscates the contents within your batch file and puts it in the generated executable.\nThe obfuscated batch code is stored in \"obfuscated_source.bat\"", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CompressFile()
        {
            // Compression ALGO's (bytepress): gzip, quicklz, lzma
            listBox1.Items.Add("Trying to compress generated EXE file...");
            if (!File.Exists("bytepress.exe"))
            {
                listBox1.Items.Add("Cannot compress EXE. \"bytepress.exe\" doesn't exist in your current directory.");
                MessageBox.Show("Cannot compress EXE. \"bytepress.exe\" doesn't exist in your current directory.", "Compression Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var processStartInfo = new ProcessStartInfo("cmd.exe", "/c title Compressing File: " + OutputP + " && @echo off && bytepress " + OutputP + " " + guna2ComboBox1.Text + " && pause")
            {
                UseShellExecute = true,
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Normal
            };
            Process p = new Process { StartInfo = processStartInfo };
            p.Start();

            string temp1 = Directory.GetCurrentDirectory() + "\\Output\\";
            string temp2 = Path.GetFileNameWithoutExtension(batch_filepath);
            temp1 += temp2;

            Thread.Sleep(1500);
            switch (File.Exists(temp1 + "_bytepressed.exe"))
            {
                case true:
                    listBox1.Items.Add("Finished compressing EXE. Output: " + temp1 + "_bytepressed.exe");
                    MessageBox.Show("Generated EXE has been compressed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case false:
                    listBox1.Items.Add("Error: Failed to compress EXE.");
                    MessageBox.Show("Failed to compress EXE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void ObfuscateBatchCode()
        {
            var bObf = new BObfuscator();
            switch (guna2ComboBox2.Text)
            {
                case "alphabet (1)":
                    listBox1.Items.Add("Obfuscating batch code using \"alphabet\" method...");
                    bObf = new BObfuscator(this.batch_cmd, BObfuscator.Methods.alphabet);
                    break;
                case "junk (2)":
                    listBox1.Items.Add("Obfuscating batch code using \"junk\" method...");
                    bObf = new BObfuscator(this.batch_cmd, BObfuscator.Methods.junk);
                    break;
                default:
                    throw new Exception("Method chosen does not exist.");
            }
            this.batch_cmd = bObf.ObfOutput;
            File.WriteAllText("Source\\" + obf_bat_file, batch_cmd);
        }

        // Compile Event
        [Obsolete]
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            batch_cmd = "";
            OutputP = "";
            OutputP += Directory.GetCurrentDirectory() + "\\Output";
            OutputP += "\\";

            string fdata;
            listBox1.Items.Add("");
            listBox1.Items.Add("Starting compilation...");
            var time = DateTime.Now;
            progressBar1.Value = 5;

            if (!bat_file_entered)
            {
                MessageBox.Show("Batch file has not been selected yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearListLog();
                progressBar1.Value = 0;
                return;
            }
            if (!File.Exists(batch_filepath))
            {
                MessageBox.Show("Batch file that was entered doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearListLog();
                progressBar1.Value = 0;
                return;
            }

            progressBar1.Value = 25;
            batch_cmd = File.ReadAllText(batch_filepath);
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            progressBar1.Value = 35;
            CompilerParameters parameters = new CompilerParameters(new string[] { "mscorlib.dll" });
            progressBar1.Value = 40;
            parameters.GenerateExecutable = true;
            string temp_out = Path.GetFileNameWithoutExtension(batch_filepath);
            OutputP += Path.GetFileNameWithoutExtension(batch_filepath);
            OutputP += ".exe";
            parameters.OutputAssembly = OutputP;
            switch (guna2CheckBox4.Checked)
            {
                case true:
                    if (!File.Exists(icon_filepath))
                    {
                        listBox1.Items.Add("Error: icon file path selected does not exist!");
                        MessageBox.Show("Icon file path does not exist!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearListLog();
                        progressBar1.Value = 0;
                        return;
                    }
                    else
                    {
                        listBox1.Items.Add("Adding file icon...");
                        parameters.CompilerOptions = @"/win32icon:" + icon_filepath;
                    }
                    break;

                case false:
                    listBox1.Items.Add("Adding default file icon...");
                    break;
            }
            
            if (guna2CheckBox3.Checked && guna2CheckBox5.Checked)
            {
                MessageBox.Show("You cannot encode your batch script with method 2.", "Option Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            File.AppendAllText(LogP, time.ToString("F") + ": Started new compilation.\nSelected Batch File: " + batch_filepath + "\nSelected Icon File: " + icon_filepath + "\n");
            try
            {
                listBox1.Items.Add("Adding dependencies to executable...");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                progressBar1.Value = 45;
                listBox1.Items.Add("Sorting out source code...");
                progressBar1.Value = 50;

                if (guna2ComboBox2.Text != "none")
                    ObfuscateBatchCode();
                if (guna2CheckBox5.Checked)
                    listBox1.Items.Add("Encoding batch code using \"bom\" method...");
                
                // Conversion
                if (guna2CheckBox2.Checked)
                    CMethods.M1(temp_out, batch_cmd, guna2CheckBox5.Checked, guna2CheckBox1.Checked);

                else if (guna2CheckBox3.Checked)
                    CMethods.M2(temp_out, batch_cmd, guna2CheckBox5.Checked, guna2CheckBox1.Checked);

                listBox1.Items.Add("Parsing batch code...");
                progressBar1.Value = 75;
                fdata = File.ReadAllText(CMethods.source_path);
            }

            catch (Exception ex1)
            {
                listBox1.Items.Add("Exception thrown: " + ex1.Message);
                File.AppendAllText(LogP, time.ToString("F") + ": Exception thrown, message: " + ex1.Message + "\n\n");
                progressBar1.Value = 0;
                return;
            }

            listBox1.Items.Add("Finally interpreting code...");
            progressBar1.Value = 85;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, fdata);
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    listBox1.Items.Add("Error: " + "Line number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber + ", '" + CompErr.ErrorText + ";" + Environment.NewLine + Environment.NewLine);
                    File.AppendAllText(LogP, time.ToString("F") + ": Error interpreting code - Line Number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber + ", '" + CompErr.ErrorText + ";" + Environment.NewLine + Environment.NewLine);
                }
                File.AppendAllText(LogP, "\n");
                MessageBox.Show("Error thrown whilst trying to interpret code.", "Compile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Value = 0;
            }

            else
            {
                progressBar1.Value = 90;
                listBox1.Items.Add("Finished Compiling.");
                listBox1.Items.Add("Output path: " + parameters.OutputAssembly);
                File.AppendAllText(LogP, time.ToString("F") + ": Finished compilation succesfully!\n\n");
                progressBar1.Value = 100;
                MessageBox.Show("Finished Compiling.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (guna2ComboBox1.Text != "none")
                    CompressFile();
            }
        }

        /*Program Info*/
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Batch is a windows only script file which consists of a series of commands to be executed by the command-line interpreter\n\n"
                + "Bat2Exe is an open source tool made for converting your batch files into a EXE file with different options and methods.\nOther features include EXE compression, and batch obfuscation.\n\n"
                + "Program last updated: " + last_updated,
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /*About Bat2Exe*/
        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: " + github_link + "\nMy Telegram: " + my_telegram + "\nOfficial Project Link: " + github_link + "/Bat2Exe", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var op = MessageBox.Show("Would you like to visit the official project link?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch(op)
            {
                case DialogResult.Yes:
                    Process.Start(github_link + "/Bat2Exe");
                    break;
            }
        }
        /*Version Check*/
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current Bat2Exe Version: " + program_version + "\nProgram last updated: " + last_updated, "Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*Options*/
        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {

            switch (File.Exists(LogP))
            {
                case true:
                    Process.Start(LogP);
                    break;

                case false:
                    MessageBox.Show("Log file does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        /*Clear Compile Log*/
        private void clearCompileLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearListLog();
            MessageBox.Show("Cleared Compile Log.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /*Update Check*/
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cleaned_response = UpdateResponse().Replace("\n", "").Replace("\r", "");
            switch (cleaned_response)
            {
                case "error":
                    return;

                case program_version:
                    MessageBox.Show("No new updates available.", "Updates Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                default:
                    var q1 = MessageBox.Show("New update is available - " + this.update_str + "\n\nWould you like to view the latest release?", "Updates Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (q1)
                    {
                        case DialogResult.Yes:
                            Process.Start(github_link + "/Bat2Exe/releases/tag/" + this.update_str);
                            break;
                    }
                    break;
            }
        }
    }
}