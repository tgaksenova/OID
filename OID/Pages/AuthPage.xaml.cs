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

namespace OID.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();

        }
        private void ButtonEnter_OnClick(Object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!!");
                return;
            }
            using (var db = new JIPEntities())
            {
                var user = db.User2.AsNoTracking().FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password == PasswordBox.Password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!!");
                }
                MessageBox.Show("Пользователь успешно найден");


                switch (user.Role)
                {
                    case "Заказчик":
                        NavigationService?.Navigate(new Menu());
                        break;

                    case "Директор":
                        NavigationService?.Navigate(new Menu());
                        break;



                }
            }

        }
        private void ButtonRegistration_OnClick (Object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/RegPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}
