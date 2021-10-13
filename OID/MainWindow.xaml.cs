using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO; 
using System.Diagnostics;
using Microsoft.Win32; 


namespace OID
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new JIPEntities())
            {
                var users = db.User2.AsNoTracking().ToList();
            }
            using (var db = new JIPEntities())
            {
                var users = db.User2.AsNoTracking().Where(u => u.Login.StartsWith("max")).ToList();
            }
            using (var db = new JIPEntities())
            {
                var users = db.User2.AsNoTracking().FirstOrDefault(u => u.Login == "max" && u.Password == "test");
            }

        }
        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"Lesson - {page.Title}";

            if (page is Pages.AuthPage)
            {
                ButtonBack.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonBack.Visibility = Visibility.Visible;
            }

        }
        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        void Export()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.ShowDialog();
            string mramor = saveFileDialog1.FileName;

            StreamWriter sw = new StreamWriter(mramor);
            JIPEntities db = new JIPEntities();
            //string IDline;
            //string Loginline;
            //string Passwordline;
            //string Roleline;
            //string FIOline;


            string IDline = String.Join(":", db.User2.Select(x => x.ID));
            sw.WriteLine(IDline);
            string Loginline = String.Join(":", db.User2.Select(x => x.Login));
            sw.WriteLine(Loginline);
            string PasswordLine = String.Join(":", db.User2.Select(x => x.Password));
            sw.WriteLine(PasswordLine);
            string Roleline = String.Join(":", db.User2.Select(x => x.Role));
            sw.WriteLine(Roleline);
            string FIOline = String.Join(":", db.User2.Select(x => x.FIO));
            sw.WriteLine(FIOline);

            sw.Close();
            Process.Start("notepad.exe", mramor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Import();
        }

        void Import()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {
                    string[] lines = new string[5];
                    for (int i = 0; i < 5; i++)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(':');
                        line = "";
                        if (data.Length >= 2)
                        {
                            for (int j = 0; j < data.Length; j++)
                            {
                                line += data[j];
                            }
                        }
                        lines[i] = line;
                    }

                    MessageBox.Show("Данные в файле: \nID: " + lines[0] + "\nФИО: " + lines[4] + "\nЛогин:    " + lines[1] + "\nПароль: " + lines[2] + "\nРоль: " + lines[3]);
                }
            }
            else MessageBox.Show("Файл для импорта не выбран!");

        }
    }
}
