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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(TextBoxPassword.Text) || string.IsNullOrEmpty(TextBoxRPassword.Text) || string.IsNullOrEmpty(TextBoxFIO.Text) || string.IsNullOrEmpty(ComboBoxRole.Text))
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                if (TextBoxPassword.Text.Length < 6)
                {
                    MessageBox.Show("Пароль должен содержать более 6 символов!");
                }
                else
                {
                    bool en = true;
                    bool number = false;
                    for (int i = 0; i < TextBoxPassword.Text.Length; i++)
                    {
                        if (TextBoxPassword.Text[i] >= 'А' && TextBoxPassword.Text[i] <= 'Я') en = false;
                        if (TextBoxPassword.Text[i] >= '0' && TextBoxPassword.Text[i] <= '9') number = true;
                    }

                    if (!en || !number)
                    {
                        MessageBox.Show("Пароль должен состоять из английских букв и иметь хотя бы одну цифру");
                    }
                    else
                    {
                        if (TextBoxPassword.Text != TextBoxRPassword.Text)
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                        else
                        {
                            JIPEntities db = new JIPEntities();


                            User2 userObject = new User2
                            {
                                Login = TextBoxLogin.Text,
                                Password = TextBoxPassword.Text,
                                FIO = TextBoxFIO.Text,
                                Role = ComboBoxRole.Text
                            };
                            db.User2.Add(userObject);
                            db.SaveChanges();

                            MessageBox.Show("Вы успешно зарегестрировались");
                        }
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            {
                NavigationService?.Navigate(new AuthPage());
            }
        }
    }
}
