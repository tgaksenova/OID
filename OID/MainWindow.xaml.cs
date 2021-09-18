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
                var users = db.User.AsNoTracking().ToList();
            }
            using (var db = new JIPEntities())
            {
                var users = db.User.AsNoTracking().Where(u => u.Login.StartsWith("max")).ToList();
            }
            using (var db = new JIPEntities())
            {
                var users = db.User.AsNoTracking().FirstOrDefault(u => u.Login == "max" && u.Password == "test");
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
    }
}
