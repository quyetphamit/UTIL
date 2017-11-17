using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_CSharp
{
    public partial class frrMain : Form
    {
        string filePath = @"D:\test.txt";
        string file;
        private SystemSetting setting;
        private string username = string.Empty;
        private string password = string.Empty;
        string pathConfig = Application.StartupPath + "\\config.xml";
        public frrMain()
        {
            InitializeComponent();
            //file = File.ReadAllText(filePath);
            //timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> lines = Common.ReadTextFile(filePath, 1, 20);
            long count = Common.counterlineTextfile(filePath);
            string temp = Common.Search(filePath, "temp");
            string content = "abc" + Environment.NewLine;
            string fileName = @"D:\abc.txt";
            //File.AppendAllText(fileName, content);
            File.Encrypt(fileName);
            Console.WriteLine(fileName);
        }

        private void frrMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "No thing";
                notifyIcon1.ShowBalloonTip(500);
                //this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameSoft = textBox1.Text;
            bool check = Common.IsRunning(nameSoft);
            Console.WriteLine(check);

        }
        private void Refreshes()
        {
            try
            {
                if (!file.Equals(File.ReadAllText(filePath)))
                {
                    List<string> lst = new List<string>() { "quyetphamit@gmail.com" };
                    StringBuilder buider = new StringBuilder();
                    buider.Append(Environment.UserDomainName);
                    buider.Append(" ");
                    buider.Append(Environment.UserName);
                    buider.Append(" ");
                    buider.Append(DateTime.Now);
                    Common.senListmail(lst, buider);
                    Console.WriteLine("Change the text file");
                    file = File.ReadAllText(filePath);
                }
                else
                {
                    Console.WriteLine("LEVIS");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Silent");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Xin chào anh quyết");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            List<History> lst = new List<History>();
            Business bs = new Business();
            lst = bs.getAll();
            foreach (var item in lst)
            {
                Console.WriteLine(item.id + " " + item.date.ToString() + " " + item.author);
            }
            History history = new History();
            //history.author = "Pham Van Quyet";
            //history.date = DateTime.Now;
            //history.type = "User";
            //bs.save(history);
            history = bs.getLast();
            Console.WriteLine(history.id + " " + history.date.ToString("HH:mm:ss") + " " + history.author + " " + history.type);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> result = Common.findEmail(filePath);
        }

        private void frrMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists(pathConfig))
            {
                SaveInitSystem();
            }
            SystemSetting.ReadXML<SystemSetting>(out setting, pathConfig);
            this.username = setting._username;
            this.password = setting._password;
        }
        public void SaveInitSystem()
        {
            setting = new SystemSetting();
            setting._username = "admin";
            setting._password = "1111";
            SystemSetting.WriteXML<SystemSetting>(this.setting, pathConfig);
        }
    }
}
