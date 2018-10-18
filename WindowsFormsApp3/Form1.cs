using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace WindowsFormsApp3
{
    public partial class Helpdesk : Form
    {
        public Helpdesk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            string url = "http://" + textBox1.Text + ":9000/api/kkm/status";
            var webClient = new WebClient();
            {
                string response = webClient.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(response);
                string newString = Encoding.UTF8.GetString(bytes);
                textBox2.Text = newString;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://" + textBox1.Text + ":9000/api/kkm/unsent-docs";
            var webClient = new WebClient();
            {
                string response = webClient.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(response);
                string newString = Encoding.UTF8.GetString(bytes);
                textBox2.Text = newString;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "http://" + textBox1.Text + ":9000/api/kkm/factory-number";
            var webClient = new WebClient();
            {
                string response = webClient.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(response);
                string newString = Encoding.UTF8.GetString(bytes);
                textBox2.Text = newString;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://" + textBox1.Text + ":9000/api/kkm/version";
            var webClient = new WebClient();
            {
                string response = webClient.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(response);
                string newString = Encoding.UTF8.GetString(bytes);
                textBox2.Text = newString;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = "http://" + textBox1.Text + ":9000/api/pinpad/status";
            var webClient = new WebClient();

            {
                string response = webClient.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(response);
                string newString = Encoding.UTF8.GetString(bytes);
                textBox2.Text = newString;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Копируем файлы..";
            try
            { 
                string fileName = "installer.exe";
                string sourcePath = @"C:\helpdesk\";
                string targetPath = @"\\" + textBox1.Text + @"\C$\install\";

                // Use Path class to manipulate file and directory paths.
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(targetPath))
                {
                System.IO.Directory.CreateDirectory(targetPath);
                }

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);

                // To copy all the files in one directory to another directory.
                // Get the files in the source folder. (To recursively iterate through
                // all subfolders under the current directory, see
                // "How to: Iterate Through a Directory Tree.")
                // Note: Check for target path was performed previously
                //       in this code example.
                if (System.IO.Directory.Exists(sourcePath))
                     {
                     string[] files = System.IO.Directory.GetFiles(sourcePath);

                    // Copy the files and overwrite destination files if they already exist.
                     foreach (string s in files)
                     {
                     // Use static Path methods to extract only the file name from the path.
                     fileName = System.IO.Path.GetFileName(s);
                     destFile = System.IO.Path.Combine(targetPath, fileName);
                     System.IO.File.Copy(s, destFile, true);
                     }
                     textBox2.Text = "Скопирован installer.exe!";
                }
                else
                {
                textBox2.Text = "Source path does not exist!";
                }
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки файла!");
                return;
            }
            
            
           
                Process process = new Process();
                // Configure the process using the StartInfo properties.
                process.StartInfo.FileName = "c:\\helpdesk\\psexec.exe";
                process.StartInfo.Arguments = "-s \\\\" + textBox1.Text + " c:\\install\\installer.exe";          
            //     process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
               if (process.ExitCode = null)
               {
                     textBox2.Text = "Установлено!";
               }    
               else
               {
                     textBox2.Text = "Ошибка!";
               }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c sc \\\\" + textBox1.Text + " stop wildberries_GoRPS";
                 process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            //     process.WaitForExit();
            textBox2.Text = "Остановлена!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки файла!");
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                // Configure the process using the StartInfo properties.
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c sc \\\\" + textBox1.Text + " start wildberries_GoRPS";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                //     process.WaitForExit();
                textBox2.Text = "Запущена!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки файла!");
                return;
            }
        }
    }
}
