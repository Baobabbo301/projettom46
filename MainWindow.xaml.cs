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

namespace stoneFenceProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OVZDatabaseEntities oVZ;
        private int _id;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ProcessAuthentication(object sender, RoutedEventArgs e)
        {
            try
            {
                oVZ = new OVZDatabaseEntities();
                string role = (from user in oVZ.Users where user.username == UserInputBox.Text && user.password == PasswordInputBox.Text select user.role).FirstOrDefault();
                int id = (from user in oVZ.Users where user.username == UserInputBox.Text && user.password == PasswordInputBox.Text select user.id).FirstOrDefault() ;
                _id = id;

                switch (role)
                {
                    case "admin":
                        LaunchAdminWindow();
                        CloseCurrentWindow();
                        break;
                    case "employee":
                        LaunchEmployeeWindow();
                        CloseCurrentWindow();
                        break;
                    default:
                        DisplayErrorMessage("Неверные учетные данные. Попробуйте еще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private void HandleTextBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "xxxxxxx" || textBox.Text == "xxxxxxx")
            {
                textBox.Text = string.Empty;
            }
        }

        private void HandleTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                string tag = (string)textBox.Tag;
                textBox.Text = tag == "U" ? "xxxxxxx" : "xxxxxxx";
            }
        }

        private void LaunchAdminWindow()
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }

        private void LaunchEmployeeWindow()
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.GetUserID(_id);
            employeeWindow.Show();
        }

        private void CloseCurrentWindow()
        {
            this.Close();
        }

        private void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
