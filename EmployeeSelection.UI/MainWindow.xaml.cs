using EmployeeSelection.Core.Services;
using System.Windows;

namespace EmployeeSelection.UI
{
    public partial class MainWindow : Window
    {
        private AuthenticationService _authService;

        public MainWindow()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = _authService.Login(txtUsername.Text, txtPassword.Password);
                MessageBox.Show($"Добро пожаловать, {user.Username}!");
                lblError.Content = "";
                txtUsername.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                // Показываем ошибку в lblError
                lblError.Content = ex.Message;

                // ДЛЯ ОТЛАДКИ: выводим в консоль
                System.Diagnostics.Debug.WriteLine($"Ошибка входа: {ex.Message}");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = _authService.RegisterUser(txtUsername.Text, txtPassword.Password, "User");
                MessageBox.Show($"Пользователь {user.Username} зарегистрирован!");
                lblError.Content = "";
                txtUsername.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;
                System.Diagnostics.Debug.WriteLine($"Ошибка регистрации: {ex.Message}");
            }
        }
    }
}