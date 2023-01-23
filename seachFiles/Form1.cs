using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace seachFiles
{
    public partial class Form1 : Form
    {
        List<string> fileName = new List<string>();
        StringBuilder printAllInfo = new StringBuilder();
        public string printFileLength(long fileByte)
        {
            if (fileByte < 1024)
            {
                return "" + fileByte + " 字节";
            }
            else if (fileByte >= 1024 && fileByte < 1048576)
            {
                return "" + (Math.Round(Convert.ToDouble(fileByte)/ 1024,3)) + " KB";
            }
            else if (fileByte >= 1048576 && fileByte < 1073741824)
            {
                return "" + (Math.Round(Convert.ToDouble(fileByte) / 1048576, 3)) + " MB";
            }
            else
            {
                return "" + (Math.Round(Convert.ToDouble(fileByte) / 1073741824, 3)) + " GB";
            }
        }
        public long getFileLength(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            long len;
            len = fi.Length;
            fi = null;
            return len;
        }
        public void printRichBox()
        {
            printAllInfo.Clear();
            foreach(var fileN in fileName)
            {
                try
                {
                    FileInfo fi = new FileInfo(fileN);
                    if (comboBox1.Text == "" || comboBox1.Text == "无筛选要求")
                    {
                        printAllInfo.Append("\n文件：" + fileN + "  大小：" + printFileLength(fi.Length) + "\n");
                    }
                    else if (comboBox1.Text == "500MB以上" && fi.Length >= 524288000)
                    {
                        printAllInfo.Append("\n文件：" + fileN + "  大小：" + printFileLength(fi.Length) + "\n");
                    }
                    else if (comboBox1.Text == "1GB以上" && fi.Length >= 1073741824)
                    {
                        printAllInfo.Append("\n文件：" + fileN + "  大小：" + printFileLength(fi.Length) + "\n");
                    }
                    else if (comboBox1.Text == "5GB以上" && fi.Length >= 5368709120)
                    {
                        printAllInfo.Append("\n文件：" + fileN + "  大小：" + printFileLength(fi.Length) + "\n");
                    }
                    else if (comboBox1.Text == "10GB以上" && fi.Length >= 10737418240)
                    {
                        printAllInfo.Append("\n文件：" + fileN + "  大小：" + printFileLength(fi.Length) + "\n");
                    }
                    fi = null;
                }
                catch(Exception ex)
                {
                    richTextBox1.Text = "错误！" + ex.Message + "错误文件：" + fileN + "\n";
                }
            }
            progressBar1.PerformStep();
            if(printAllInfo.ToString() == "")
            {
                richTextBox1.Text = "未能找到符合条件的内容！也可能是文件夹里全是不大不小的文件，未达到您的筛选要求！";
                progressBar1.PerformStep();
            }
            else
            {
                richTextBox1.Text += printAllInfo;
                progressBar1.PerformStep();
            }
        }
        public void folerSeach(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileSystemInfo[] fsif = dir.GetFileSystemInfos();
            foreach(var x in fsif)
            {
                if(x is DirectoryInfo)
                {
                    folerSeach(x.FullName);
                }
                else
                {
                    fileName.Add(x.FullName);
                }
            }
            progressBar1.PerformStep();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("请选择一个文件夹路径。");
            }
            else
            {
                progressBar1.Value = 0;
                fileName.Clear();
                richTextBox1.Text = "正在检索中，如果文件太多会导致检索速度很慢....";
                progressBar1.PerformStep();
                folerSeach(textBox1.Text);
                printRichBox();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            comboBox1.Location = new Point(this.Width / 2,(this.Height / 2) - 150);
            label1.Location = new Point(label2.Location.X - 95, label2.Location.Y - 54);
            textBox1.Location = new Point(label2.Location.X , label2.Location.Y - 58);
            button1.Location = new Point(label2.Location.X + 190, label2.Location.Y - 58);
            label2.Location = new Point((this.Width / 2) - 100, (this.Height / 2) - 148);
            button2.Location = new Point((this.Width / 2) + 105, (this.Height / 2) - 152);
            progressBar1.Location = new Point((this.Width / 2) - 50, richTextBox1.Location.Y + richTextBox1.Size.Height + 10);
            label3.Location = new Point((this.Width / 2) - 115, richTextBox1.Location.Y + richTextBox1.Size.Height + 12);
            richTextBox1.Size = new Size(this.Width - 150,this.Height / 2);
            richTextBox1.Location = new Point(richTextBox1.Location.X,comboBox1.Location.Y + 50);
        }
    }
}
